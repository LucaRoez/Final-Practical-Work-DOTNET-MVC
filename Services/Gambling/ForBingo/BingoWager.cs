using TrabajoFinal___Bingo_Web_MVC_Service.Services.Extensions;
using TrabajoFinal___Bingo_Web_MVC_Service.Data.Repository;
using TrabajoFinal___Bingo_Web_MVC_Service.Data.Entities;

namespace TrabajoFinal___Bingo_Web_MVC_Service.Services.Gambling.ForBingo
{
    public class BingoWager : StartBingoGamble, IWager
    {
        private readonly IRepository _repository;
        private readonly IChanceMechanism _drawLottery;
        private readonly IStructureConstructor _structureConstructor;

        public BingoWager(IRepository repository, IStructureConstructor structureConstructor, IDataModelExchange dataModelExchange, IChanceMechanism drawLottery)
            : base(repository, structureConstructor, dataModelExchange)
        {
            _repository = repository;
            _drawLottery = drawLottery;
            _structureConstructor = structureConstructor;
        }

        public List<Bingo> RecoverBingoData(int players)
        {
            List<Bingo> allBingos = _repository.GetAllBingos(); allBingos.Reverse();
            List<Bingo> dataList = allBingos.Take(players).ToList(); dataList.Reverse();

            return dataList;
        }

        public List<Bingo> GetNewDrawLot(List<Bingo> dataList)
        {
            string numberTossed;
            switch (dataList[0].Lottery)
            {
                case "": numberTossed = _drawLottery.GetNewDrawLot(); break;
                default: numberTossed = _drawLottery.GetNewDrawLot(dataList[0].Lottery); break;
            }
            dataList.ForEach(data =>
            {
                if (data.Lottery == "") { data.Lottery = numberTossed; } else { data.Lottery += " - " + numberTossed; }
            });

            return CountHits(dataList, int.Parse(numberTossed));
        }

        private List<Bingo> CountHits(List<Bingo> dataList, int numberTossed)
        {
            foreach (var data in dataList)
            {
                List<int> numberList = _structureConstructor.GetMatrixInt(data.Numbers);
                numberList.ForEach(n =>
                {
                    if (n == numberTossed)
                    {
                        if (data.WinningNumbers != "") { data.WinningNumbers += " - " + numberTossed; }
                        else { data.WinningNumbers += numberTossed; }
                        data.Hits++; return;
                    }
                });
            }

            return dataList;
        }
    }
}
