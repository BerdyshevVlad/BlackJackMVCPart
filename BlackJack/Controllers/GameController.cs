using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BlackJack.BusinessLogic.Services;
using BlackJack.ViewModels;
using ExceptionLoggers;

namespace BlackJack.Controllers
{
    public class GameController : Controller
    {
        private readonly GameService _gameService;

        public GameController(GameService gameService)
        {
            _gameService = gameService;
        }


        [ExceptionLogger]
        public async Task<ActionResult> Start()
        {
            await _gameService.SetDeck();

            return View();
        }


        [ExceptionLogger]
        [HttpPost]
        public async Task<ActionResult> Start(int botCount)
        {
            StartGameView model = await _gameService.Start(botCount);

            return View("Play",model.Players);
        }


        [ExceptionLogger]
        [HttpPost]
        public async Task<ActionResult> More()
        {
            MoreOrEnoughGameView model =await _gameService.More();

            return PartialView("MoreOrEnough",model.Players);
        }


        [ExceptionLogger]
        [HttpPost]
        public async Task<ActionResult> Enough()
        {
            MoreOrEnoughGameView model = await _gameService.Enough();

            return PartialView("MoreOrEnough", model.Players);
        }


        [ExceptionLogger]
        [HttpGet]
        public async Task<ActionResult> History()
        {
            HistoryGameView model=await _gameService.GetHistory();

            return PartialView("History",model.Players);
        }

    }
}