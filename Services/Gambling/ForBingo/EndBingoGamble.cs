using TrabajoFinal___Bingo_Web_MVC_Service.Data.Entities;
using TrabajoFinal___Bingo_Web_MVC_Service.Data.Repository;
using TrabajoFinal___Bingo_Web_MVC_Service.Models;
using TrabajoFinal___Bingo_Web_MVC_Service.Services.Extensions;

namespace TrabajoFinal___Bingo_Web_MVC_Service.Services.Gambling.ForBingo
{
    public class EndBingoGamble : BingoWager, IEndGamble
    {
        private readonly IRepository _repository;
        private readonly IDataModelExchange _dataModelExchange;

        public EndBingoGamble(IRepository repository, IStructureConstructor structureConstructor, IDataModelExchange dataModelExchange, IChanceMechanism drawLottery)
            : base(repository, structureConstructor, dataModelExchange, drawLottery)
        {
            _repository = repository;
            _dataModelExchange = dataModelExchange;
        }

        public async Task<List<BingoViewModel>> IsBingoEnd(List<Bingo> dataList)
        {
            dataList.ForEach(data =>
            { if (data.Hits == 15) { data.IsWinner = true; return; } }
            );
            await _repository.Update(dataList);

            return _dataModelExchange.GetBingosModel(dataList);
        }
    }
}
