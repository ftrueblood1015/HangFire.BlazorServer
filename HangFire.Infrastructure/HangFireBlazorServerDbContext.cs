using HangFire.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HangFire.Infrastructure
{
    public class HangFireBlazorServerDbContext : DbContext
    {
        public HangFireBlazorServerDbContext(DbContextOptions<HangFireBlazorServerDbContext> options) : base(options) { }

        DbSet<House> Houses => Set<House>();
        DbSet<MtgCard> MtgCards => Set<MtgCard>();
    }
}
