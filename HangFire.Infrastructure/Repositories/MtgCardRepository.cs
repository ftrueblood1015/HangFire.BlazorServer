using HangFire.Domain.Entities;
using HangFire.Domain.Interfaces.Repositories;

namespace HangFire.Infrastructure.Repositories
{
    public class MtgCardRepository : RepositoryBase<MtgCard, HangFireBlazorServerDbContext>, IMtgCardRepository
    {
        public MtgCardRepository(HangFireBlazorServerDbContext context) : base(context)
        {
        }
    }
}
