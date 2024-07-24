using HangFire.Domain.Interfaces.Services;
using Hangfire;
using HangFire.Domain.Entities;
using HangFire.Domain.Models;

namespace HangFire.JobServer.Jobs
{
    public class MultipleRandomScryfallCards
    {
        protected readonly IMtgCardService CardService;

        protected readonly IScryfallService ScryfallService;

        public static string JobName = "Import Multiple Cards From Scryfall";

        public static string CronExpress = Cron.Minutely();

        public MultipleRandomScryfallCards(IMtgCardService mtgCardService, IScryfallService scryfallService)
        {
            CardService = mtgCardService;
            ScryfallService = scryfallService;
        }

        public async Task RunJob()
        {
            Task<ScryfallMtgCard> random;
            int count = 0;
            int randomNumber = GetRandomNumber();

            while (count < randomNumber)
            {
                random = GetRandomCard();
                SaveRandomCard(await Task.FromResult(random).Result);
                count++;
            }
        }

        public async Task<ScryfallMtgCard> GetRandomCard()
        {
            return await ScryfallService.GetRandomCard();
        }

        public void SaveRandomCard(ScryfallMtgCard card)
        {
            try
            {
                MtgCard mtgCard = new MtgCard();
                mtgCard = card.ScryfallTransform();
                BackgroundJob.Enqueue(() => AddCardIfNotExists(mtgCard));
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex}");
            }
        }

        public void AddCardIfNotExists(MtgCard card)
        {
            var filteredCards = CardService.Filter(x => x.OracleId == card.OracleId);

            if (!filteredCards.Any())
            {
                BackgroundJob.Enqueue(() => CardService.Add(card));
            }
            else
            {
                Console.WriteLine($"{card.Name} already exists");
            }
        }

        public int GetRandomNumber()
        {
            Random randomInt = new Random();
            return randomInt.Next(1, 15);
        }
    }
}
