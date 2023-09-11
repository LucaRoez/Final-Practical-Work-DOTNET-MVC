using TrabajoFinal___Bingo_Web_MVC_Service.Models;
using TrabajoFinal___Bingo_Web_MVC_Service.Services.Extensions;
using TrabajoFinal___Bingo_Web_MVC_Service.Data.Repository;

namespace TrabajoFinal___Bingo_Web_MVC_Service.Services.Gambling.ForBingo
{
    public class StartBingoGamble : IStartGamble
    {
        private readonly IRepository _repository;
        private readonly IStructureConstructor _structureConstructor;
        private readonly IDataModelExchange _dataModelExchange;

        public StartBingoGamble(IRepository repository, IStructureConstructor structureConstructor, IDataModelExchange dataModelExchange)
        {
            _repository = repository;
            _structureConstructor = structureConstructor;
            _dataModelExchange = dataModelExchange;
        }

        public List<BingoViewModel> CreateBingos(int ChosenPlayers)
        {
            var modelList = new List<BingoViewModel>();
            for (int i = 1; i <= ChosenPlayers; i++) { modelList.Add(_structureConstructor.GetNewBingo(i)); }

            return modelList;
        }

        public bool CheckUniqueNumbers(List<BingoViewModel> bingoList)
        {
            foreach (var bingo in bingoList)
            {
                string newString = ""; List<int> bingoNumbers = bingo.MatrixModel;
                for (int n = 0; n < 27; n++)
                {
                    if (n != 26) { newString += bingoNumbers[n] + "_"; }
                    else { newString += bingoNumbers[n]; }
                }
                newString = newString.Replace("-1", ""); string[] stringList = newString.Split('_');
                List<int> numberList = new(); int i = 0; 
                while (i < 27) { if (stringList[i] != "") { numberList.Add(int.Parse(stringList[i])); } i++; }

                foreach (int num in numberList) { if (numberList.FindAll(n => n == num).Count > 1) { return false; } }
            }

            return true;
        }

        public bool VerifyCorrectRange(List<BingoViewModel> bingoList)
        {
            foreach (var bingo in bingoList)
            {
                for (int i = 0; i < 27; i++)
                {
                    if (i == 0 || i == 9 || i == 18) { if (bingo.MatrixModel[i] < 0 || bingo.MatrixModel[i] > 9) { return false; } }
                    else if (i == 1 || i == 10 || i == 19) { if (bingo.MatrixModel[i] < 10 || bingo.MatrixModel[i] > 19) { return false; } }
                    else if (i == 2 || i == 11 || i == 20) { if (bingo.MatrixModel[i] < 20 || bingo.MatrixModel[i] > 29) { return false; } }
                    else if (i == 3 || i == 12 || i == 21) { if (bingo.MatrixModel[i] < 30 || bingo.MatrixModel[i] > 39) { return false; } }
                    else if (i == 4 || i == 13 || i == 22) { if (bingo.MatrixModel[i] < 40 || bingo.MatrixModel[i] > 49) { return false; } }
                    else if (i == 5 || i == 14 || i == 23) { if (bingo.MatrixModel[i] < 50 || bingo.MatrixModel[i] > 59) { return false; } }
                    else if (i == 6 || i == 15 || i == 24) { if (bingo.MatrixModel[i] < 60 || bingo.MatrixModel[i] > 69) { return false; } }
                    else if (i == 7 || i == 16 || i == 25) { if (bingo.MatrixModel[i] < 70 || bingo.MatrixModel[i] > 79) { return false; } }
                    else { if (bingo.MatrixModel[i] < 80 || bingo.MatrixModel[i] > 90) { return false; } }
                }
            }

            return true;
        }

        public async Task<List<BingoViewModel>> SealNewBingos(List<BingoViewModel> modelList)
        {
            modelList = _structureConstructor.GetBingoModelData(modelList);
            await _repository.Insert(_dataModelExchange.GetBingosData(modelList));

            return modelList;
        }
    }
}
