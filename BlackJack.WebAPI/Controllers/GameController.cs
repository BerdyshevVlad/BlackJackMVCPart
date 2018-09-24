using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.ViewModels;
using ExceptionLoggers;
using Newtonsoft.Json;

namespace BlackJack.WebApi.Controllers
{
    [RoutePrefix("api/game")]
    public class GameController : ApiController
    {
        private readonly IGameService _gameService;
        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }


        [HttpPost]
        public async Task<StartGameView> Start([FromBody] SetNameAndBotCount json)
        {

            StartGameView model = await _gameService.Start(json);

            return model;
        }



        [HttpGet]
        [Route("more")]
        [ResponseType(typeof(MoreGameView))]
        public async Task<MoreGameView> More()
        {
            MoreGameView model = await _gameService.More();

            return model;
        }



        [HttpGet]
        [Route("enough")]
        [ResponseType(typeof(EnoughGameView))]
        public async Task<EnoughGameView> Enough()
        {
            EnoughGameView model = await _gameService.Enough();

            return model;
        }


        [HttpGet]
        [Route("history")]
        [ResponseType(typeof(HistoryGameView))]
        public async Task<HistoryGameView> History()
        {
            HistoryGameView model = await _gameService.GetHistory();

            return model;
        }
    }
}
