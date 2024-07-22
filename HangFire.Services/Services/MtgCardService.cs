using AutoMapper;
using HangFire.Domain.Entities;
using HangFire.Domain.Interfaces.Repositories;
using HangFire.Domain.Interfaces.Services;

namespace HangFire.Services.Services
{
    public class MtgCardService : ServiceBase<MtgCard, IMtgCardRepository>, IMtgCardService
    {
        public MtgCardService(IMtgCardRepository repo, IMapper mapper) : base(repo, mapper)
        {
        }
    }
}
