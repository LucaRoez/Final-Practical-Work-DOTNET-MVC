using TrabajoFinal___Bingo_Web_MVC_Service.Data.Entities;

namespace TrabajoFinal___Bingo_Web_MVC_Service.Data.Repository
{
    public interface IRepository
    {
        List<Bingo> GetAllBingos();
        Bingo? GetBingoById(int id);
        Task Insert(List<Bingo> Bingos);
        Task Update(List<Bingo> Bingos);
        Task Delete(List<Bingo> Bingos);
    }
}
