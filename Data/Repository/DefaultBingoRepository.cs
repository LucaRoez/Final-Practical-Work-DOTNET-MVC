
using Microsoft.EntityFrameworkCore;
using TrabajoFinal___Bingo_Web_MVC_Service.Data;
using TrabajoFinal___Bingo_Web_MVC_Service.Data.Entities;

namespace TrabajoFinal___Bingo_Web_MVC_Service.Data.Repository
{
    public class DefaultBingoRepository : IRepository
    {
        private readonly BingoDbContext _dbContext;
        public DefaultBingoRepository(BingoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Bingo> GetAllBingos() => _dbContext.Bingos.ToList();

        public Bingo? GetBingoById(int id) => _dbContext.Bingos.Find(id);

        public async Task Insert(List<Bingo> Bingos)
        {
            _dbContext.Bingos.AddRange(Bingos);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(List<Bingo> Bingos)
        {
            _dbContext.Bingos.UpdateRange(Bingos);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(List<Bingo> Bingos)
        {
            _dbContext.Bingos.RemoveRange(Bingos);
            await _dbContext.SaveChangesAsync();
        }
    }
}
