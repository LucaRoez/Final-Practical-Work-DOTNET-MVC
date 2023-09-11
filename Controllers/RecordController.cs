using Microsoft.AspNetCore.Mvc;
using TrabajoFinal___Bingo_Web_MVC_Service.Data.Entities;
using TrabajoFinal___Bingo_Web_MVC_Service.Data.Repository;
using TrabajoFinal___Bingo_Web_MVC_Service.Services.Extensions;

namespace TrabajoFinal___Bingo_Web_MVC_Service.Controllers
{
    public class RecordController : Controller
    {
        private readonly IRepository _repository;
        private readonly IDataModelExchange _dataModelExchange;
        public RecordController(IRepository repository, IDataModelExchange dataModelExchange)
        {
            _repository = repository;
            _dataModelExchange = dataModelExchange;
        }

        public IActionResult BingoWinners()
        {
            List<Bingo> winners = new();
            foreach (Bingo b in _repository.GetAllBingos())
            {
                if (b.IsWinner) { winners.Add(b); }
            }

            return View(_dataModelExchange.GetBingosModel(winners));
        }

        public IActionResult BingoList() => View(_dataModelExchange.GetBingosModel(_repository.GetAllBingos())); 

        public IActionResult BingoSet(int id) => View(_dataModelExchange.GetBingosModel(_dataModelExchange.GetBingoSet(id)));

        public IActionResult BingoDetail(int id) => View(_dataModelExchange.GetBingoModel(id));

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            await _repository.Delete(_dataModelExchange.GetBingoSet(id));

            return View("BingoList", _dataModelExchange.GetBingosModel(_repository.GetAllBingos()));
        }
    }
}
