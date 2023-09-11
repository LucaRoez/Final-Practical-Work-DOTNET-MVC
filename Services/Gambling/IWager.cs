using TrabajoFinal___Bingo_Web_MVC_Service.Data.Entities;

namespace TrabajoFinal___Bingo_Web_MVC_Service.Services.Gambling
{
    public interface IWager
    {
        List<Bingo> RecoverBingoData(int players);
        List<Bingo> GetNewDrawLot(List<Bingo> dataList);
    }
}
