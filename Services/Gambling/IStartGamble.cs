using TrabajoFinal___Bingo_Web_MVC_Service.Models;

namespace TrabajoFinal___Bingo_Web_MVC_Service.Services.Gambling
{
    public interface IStartGamble
    {
        List<BingoViewModel> CreateBingos(int ChosenPlayers);
        bool CheckUniqueNumbers(List<BingoViewModel> numbersList);
        bool VerifyCorrectRange(List<BingoViewModel> numbersList);
        Task<List<BingoViewModel>> SealNewBingos(List<BingoViewModel> modelList);
    }
}
