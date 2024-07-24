using Hangfire;
using HangFire.Domain.Entities;
using HangFire.Domain.Interfaces.Services;
using HangFire.Domain.Models;

namespace HangFire.JobServer.Jobs
{
    public class RandomScryfallCard
    {
        protected readonly IMtgCardService CardService;

        protected readonly IScryfallService ScryfallService;

        public static string JobName = "Import Card From Scryfall";

        public static string CronExpress = Cron.Minutely();

        public RandomScryfallCard(IMtgCardService mtgCardService, IScryfallService scryfallService)
        {
            CardService = mtgCardService;
            ScryfallService = scryfallService;
        }

        public async Task RunJob()
        {
            var random = GetRandomCard();
            SaveRandomCard(await Task.FromResult(random).Result);
        }

        private async Task<ScryfallMtgCard> GetRandomCard()
        {
            return await ScryfallService.GetRandomCard();
        }

        private void SaveRandomCard(ScryfallMtgCard card)
        {
            try
            {
                MtgCard mtgCard = new MtgCard();
                mtgCard = card.ScryfallTransform();
                BackgroundJob.Enqueue(() => CardService.Add(mtgCard));
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex}");
            }
        }
    }
}
