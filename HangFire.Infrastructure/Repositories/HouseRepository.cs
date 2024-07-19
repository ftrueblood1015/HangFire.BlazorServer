using HangFire.Domain.Entities;
using HangFire.Domain.Interfaces.Repositories;

namespace HangFire.Infrastructure.Repositories
{
    public class HouseRepository : RepositoryBase<House, HangFireBlazorServerDbContext>, IHouseRepository
    {
        public HouseRepository(HangFireBlazorServerDbContext context) : base(context)
        {
        }
    }
}
