using TrabajoFinal___Bingo_Web_MVC_Service.Models;

namespace TrabajoFinal___Bingo_Web_MVC_Service.Services.Extensions
{
    public interface IStructureConstructor
    {
        BingoViewModel GetNewBingo(int playerNumber);
        List<int> GetMatrixInt(string matrix);
        List<BingoViewModel> GetBingoModelData(List<BingoViewModel> modelList);
    }
}
