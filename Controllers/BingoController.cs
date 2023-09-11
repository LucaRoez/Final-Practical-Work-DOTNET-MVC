using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TrabajoFinal___Bingo_Web_MVC_Service.Models;
using TrabajoFinal___Bingo_Web_MVC_Service.Services.Gambling;

namespace TrabajoFinal___Bingo_Web_MVC_Service.Controllers
{
    public class BingoController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStartGamble _startGamble;
        private readonly IWager _wager;
        private readonly IEndGamble _endGamble;
        public BingoController(ILogger<HomeController> logger, IStartGamble startGamble, IWager wager, IEndGamble endGamble)
        {
            _logger = logger;
            _startGamble = startGamble;
            _wager = wager;
            _endGamble = endGamble;
        }

        public IActionResult FillBingos(int players) => View(_startGamble.CreateBingos(players));
        public IActionResult FillBingosFailed()
        {
            if (TempData.TryGetValue("InvalidModels", out var jsonModels) &&
                jsonModels is string jsonModelsString)
            {
                var models = JsonConvert.DeserializeObject<List<BingoViewModel>>(jsonModelsString);

                switch (TempData["FlawType"])
                {
                    case 492: ViewData["Missing"] = "CorrectRange";
                        ViewData["InvalidationMessage"] = "Must respet the range numbers order, the client-side hacking is forbidden, please cease to do it or we call the pertinent authorities."; break;
                    case 486: ViewData["Missing"] = "UniqueNumbers";
                        ViewData["InvalidationMessage"] = "There're repeated numbers, please do not choose same number, one unique per square cell"; break;
                }
                
                return View(models);
            }
            return RedirectToAction("FillBingos", (int)TempData["PlayersCount"]);
        }

        [HttpPost]
        public async Task<IActionResult> Shipping(List<BingoViewModel> models)
        {
            if (!ModelState.IsValid) { return RedirectToAction("FillBingos", models.Count); }
            else if (models == null || models.Count == 0) { return RedirectToAction("StartBingo", "Home"); }

            if (_startGamble.CheckUniqueNumbers(models))
            {
                if (_startGamble.VerifyCorrectRange(models))
                {
                    models = await _startGamble.SealNewBingos(models);
                    return View("BingoGambling", models);
                }
                else
                {
                    InputsFailure(models); TempData["FlawType"] = 492;
                    return RedirectToAction("FillBingosFailed");
                }
            }
            else
            {
                InputsFailure(models); TempData["FlawType"] = 486;
                return RedirectToAction("FillBingosFailed");
            }
        }
        private void InputsFailure(List<BingoViewModel> models)
        {
            var jsonModels = JsonConvert.SerializeObject(models);
            TempData["InvalidModels"] = jsonModels;
            TempData["PlayersCount"] = models.Count;
        }

        [HttpPost]
        public async Task<IActionResult> BingoGambling(int players) => View(await _endGamble.IsBingoEnd(_wager.GetNewDrawLot(_wager.RecoverBingoData(players))));
    }
}
