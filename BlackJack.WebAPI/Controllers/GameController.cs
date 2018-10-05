using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using BlackJack.BusinessLogic.Interfaces;
using BlackJack.ViewModels;

namespace BlackJack.WebApi.Controllers
{

    [RoutePrefix("api/game")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GameController : ApiController
    {
        private readonly IGameService _gameService;
        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }


        [HttpPost]
        [Route("start")]
        public async Task<IHttpActionResult> Start([FromBody] SetNameAndBotCount json)
        {
            StartGameView model =new StartGameView();
            try
            {
                 model = await _gameService.Start(json);
            }
            catch (Exception e)
            {
                BadRequest(e.Message);
            }
           
            return Ok(model);
        }



        [HttpGet]
        [Route("more")]
        public async Task<IHttpActionResult> More()
        {
            MoreGameView model =new MoreGameView();
            try
            {
                model = await _gameService.More();
            }
            catch (Exception e)
            {
                BadRequest(e.Message);
            }

            return Ok(model);
        }



        [HttpGet]
        [Route("enough")]
        public async Task<IHttpActionResult> Enough()
        {
            EnoughGameView model =new EnoughGameView();
            try
            {
                model = await _gameService.Enough();
            }
            catch (Exception e)
            {
                BadRequest(e.Message);
            }

            return Ok(model);
        }



        [HttpGet]
        [Route("history")]
        public async Task<IHttpActionResult> History()
        {
            HistoryGameView model =new HistoryGameView();
            try
            {
                model = await _gameService.GetHistory();
            }
            catch (Exception e)
            {
                BadRequest(e.Message);
            }

            return Ok(model);
        }
    }
}
