using TrabajoFinal___Bingo_Web_MVC_Service.Data.Entities;
using TrabajoFinal___Bingo_Web_MVC_Service.Models;

namespace TrabajoFinal___Bingo_Web_MVC_Service.Services.Extensions
{
    public interface IDataModelExchange
    {
        List<Bingo> GetBingosData(List<BingoViewModel> modelList);
        List<BingoViewModel> GetBingosModel(List<Bingo> dataList);
        List<Bingo> GetBingoSet(int id);
        BingoViewModel GetBingoModel(int id);
    }
}