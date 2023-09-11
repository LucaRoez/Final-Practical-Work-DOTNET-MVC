using TrabajoFinal___Bingo_Web_MVC_Service.Data.Entities;
using TrabajoFinal___Bingo_Web_MVC_Service.Data.Repository;
using TrabajoFinal___Bingo_Web_MVC_Service.Models;

namespace TrabajoFinal___Bingo_Web_MVC_Service.Services.Extensions.ForBingo
{
    public class BingoDataModelExchange : IDataModelExchange
    {
        private readonly IRepository _repository;
        private readonly IStructureConstructor _structureConstructor;
        public BingoDataModelExchange(IRepository repository, IStructureConstructor structureConstructor)
        {
            _repository = repository;
            _structureConstructor = structureConstructor;
        }

        public List<Bingo> GetBingosData(List<BingoViewModel> modelList)
        {
            List<Bingo> bingosData = modelList.Select(m => new Bingo()
            {
                PlayerNumber = m.PlayerNumber,
                Matrix = m.MatrixModelString,
                Numbers = m.ChosenNumbers,
                Lottery = m.CurrentLottery,
                Hits = m.CurrentHits,
                WinningNumbers = m.CurrentWinningNumbers,
                IsWinner = m.IsWinner
            }).ToList();

            return bingosData;
        }

        public List<BingoViewModel> GetBingosModel(List<Bingo> dataList)
        {
            List<BingoViewModel> bingosModel = dataList.Select(d => new BingoViewModel()
            {
                Id = d.Id,
                PlayerNumber = d.PlayerNumber,
                MatrixModelString = d.Matrix,
                MatrixModel = _structureConstructor.GetMatrixInt(d.Matrix),
                ChosenNumbers = d.Numbers,
                CurrentLottery = d.Lottery,
                CurrentHits = d.Hits,
                CurrentWinningNumbers = d.WinningNumbers,
                IsWinner = d.IsWinner
            }).ToList();

            return bingosModel;
        }

        public List<Bingo> GetBingoSet(int id)
        {
            List<Bingo> bingoSet = new();
            Bingo? bingo = _repository.GetBingoById(id);

            if (bingo != null)
            {
                int playerNum = bingo.PlayerNumber;
                int search = 0; for (int count = playerNum; count > 1; count--) { search++; }

                int currentId = id - search; bingo = _repository.GetBingoById(currentId);       // first creates the record
                bingoSet.Add(bingo); currentId++;

                if (bingo.Id != _repository.GetAllBingos().Last().Id)       // then checks it's not the last one 
                {
                    bingo = _repository.GetBingoById(currentId);            // second updates it
                    while (bingo.PlayerNumber != 1)                         // and checks not to be a new set
                    {
                        bingoSet.Add(bingo);
                        if (bingo.Id == _repository.GetAllBingos().Last().Id) { break; }    // still checking and updating

                        currentId++;
                        bingo = _repository.GetBingoById(currentId);
                    }
                }
            }

            return bingoSet;
        }

        public BingoViewModel GetBingoModel(int id)
        {
            Bingo d = _repository.GetBingoById(id);

             return new BingoViewModel()
            {
                Id = d.Id,
                PlayerNumber = d.PlayerNumber,
                MatrixModelString = d.Matrix,
                MatrixModel = _structureConstructor.GetMatrixInt(d.Matrix),
                ChosenNumbers = d.Numbers,
                CurrentLottery = d.Lottery,
                CurrentHits = d.Hits,
                CurrentWinningNumbers = d.WinningNumbers,
                IsWinner = d.IsWinner
            };
        }
    }
}