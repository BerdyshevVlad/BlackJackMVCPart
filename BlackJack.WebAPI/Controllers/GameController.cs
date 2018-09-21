using BlackJack.BusinessLogic.Interfaces;
using BlackJack.ViewModels;
using ExceptionLoggers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace BlackJack.WebAPI.Controllers
{
    [Route("api/game")]
    public class GameController : ApiController
    {
        private readonly IGameService _gameService;
        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [ExceptionLogger]
        public IEnumerable<string> Index()
        {
            return new string[] { "value1", "value2" };
        }


        [ExceptionLogger]
        [HttpGet]
        [Route("start")]
        public async Task<StartGameView> Start()
        {
            //StartGameView model = await _gameService.Start(Int32.Parse(botCount), userName);

            //return model;
            return  new StartGameView();
        }


        [ExceptionLogger]
        [HttpPost]
        public async Task<MoreGameView> More()
        {
            MoreGameView model = await _gameService.More();

            return model;
        }


        [ExceptionLogger]
        [HttpPost]
        public async Task<EnoughGameView> Enough()
        {
            EnoughGameView model = await _gameService.Enough();

            return model;
        }


        [ExceptionLogger]
        [HttpGet]
        public async Task<HistoryGameView> History()
        {
            HistoryGameView model = await _gameService.GetHistory();

            return model;
        }
    }
}
