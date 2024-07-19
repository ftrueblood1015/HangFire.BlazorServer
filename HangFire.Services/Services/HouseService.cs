using AutoMapper;
using HangFire.Domain.Entities;
using HangFire.Domain.Interfaces.Repositories;
using HangFire.Domain.Interfaces.Services;
using HangFire.Domain.ViewModels;

namespace HangFire.Services.Services
{
    public class HouseService : ServiceBase<House, IHouseRepository>, IHouseService
    {
        public HouseService(IHouseRepository repo, IMapper mapper) : base(repo, mapper)
        {
        }
    }
}
