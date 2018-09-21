using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.ViewModels;
using ExceptionLoggers;

namespace BlackJack.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }


        [ExceptionLogger]
        public ActionResult Index()
        {
            return View();
        }


        [ExceptionLogger]
        public async Task<ActionResult> Start()
        {

            return View();
        }



        [ExceptionLogger]
        [HttpPost]
        public async Task<ActionResult> Start(string botCount,string userName)
        {
            StartGameView model = await _gameService.Start(Int32.Parse(botCount), userName);

            return PartialView("_Play",model.Players);
        }


        [ExceptionLogger]
        [HttpPost]
        public async Task<ActionResult> More()
        {
            MoreGameView model =await _gameService.More();

            return PartialView("_Play",model.Players);
        }


        [ExceptionLogger]
        [HttpPost]
        public async Task<ActionResult> Enough()
        {
            EnoughGameView model = await _gameService.Enough();

            return PartialView("_Play", model.Players);
        }


        [ExceptionLogger]
        [HttpGet]
        public async Task<ActionResult> History()
        {
            HistoryGameView model=await _gameService.GetHistory();

            return PartialView("_History",model.Players);
        }

    }
}