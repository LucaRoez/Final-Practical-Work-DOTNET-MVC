using Microsoft.EntityFrameworkCore;
using TrabajoFinal___Bingo_Web_MVC_Service.Data.Entities;

namespace TrabajoFinal___Bingo_Web_MVC_Service.Data
{
    public class BingoDbContext : DbContext
    {
        public BingoDbContext()
        {
        }
        public BingoDbContext(DbContextOptions<BingoDbContext> op)
            : base(op)
        {
        }

        public DbSet<Bingo> Bingos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
