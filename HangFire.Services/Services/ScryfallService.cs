using HangFire.Domain.Entities;
using HangFire.Domain.Interfaces.Services;
using HangFire.Domain.Models;
using System.Net.Http.Json;

namespace HangFire.Services.Services
{
    public class ScryfallService : IScryfallService
    {
        protected readonly ScryfallApiServerClient ScryfallClient;

        public ScryfallService(ScryfallApiServerClient client)
        {
            ScryfallClient = client;
        }

        public async Task<ScryfallMtgCard> GetRandomCard()
        {
            try
            {
                Thread.Sleep(75);
                var url = new Uri("cards/random", UriKind.Relative);
                var mtgCard = await ScryfallClient.GetData<ScryfallMtgCard>(url);
                return mtgCard!;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex}");
            }
        }
    }
}
