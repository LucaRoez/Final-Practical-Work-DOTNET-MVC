using TrabajoFinal___Bingo_Web_MVC_Service.Data.Entities;
using TrabajoFinal___Bingo_Web_MVC_Service.Models;

namespace TrabajoFinal___Bingo_Web_MVC_Service.Services.Gambling
{
    public interface IEndGamble
    {
        Task<List<BingoViewModel>> IsBingoEnd(List<Bingo> modelList);
    }
}
