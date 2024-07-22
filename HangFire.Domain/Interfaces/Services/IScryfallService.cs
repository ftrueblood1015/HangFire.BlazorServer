using HangFire.Domain.Models;

namespace HangFire.Domain.Interfaces.Services
{
    public interface IScryfallService
    {
        Task<ScryfallMtgCard> GetRandomCard();
    }
}
