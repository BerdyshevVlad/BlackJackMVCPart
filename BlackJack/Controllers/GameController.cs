﻿using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using BlackJack.ViewModels;
using ExceptionLoggers;
using Newtonsoft.Json;

namespace BlackJack.Controllers
{
    public class GameController : Controller
    {
        private const string baseUrl= "http://localhost:50610/";

        public GameController()
        {

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
        public async Task<ActionResult> Start(SetNameAndBotCount userNameAndBotCount)
        {

            using (var http = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(userNameAndBotCount);
                HttpContent content = new StringContent(json);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await http.PostAsync(baseUrl+"api/game/start", content);

                StartGameView model = response.Content.ReadAsAsync<StartGameView>().Result;

                return PartialView("_Play", model.Players);
            }
        }


        [ExceptionLogger]
        [HttpGet]
        public async Task<ActionResult> More()
        {
            using (var http = new HttpClient())
            {
                HttpResponseMessage response = await http.GetAsync(baseUrl+"api/game/more");

                StartGameView model = response.Content.ReadAsAsync<StartGameView>().Result;

                return PartialView("_Play", model.Players);
            }
        }


        [ExceptionLogger]
        [HttpGet]
        public async Task<ActionResult> Enough()
        {
            using (var http = new HttpClient())
            {
                HttpResponseMessage response = await http.GetAsync(baseUrl+"api/game/enough");
                string statusCode = response.StatusCode.ToString();

                StartGameView model = response.Content.ReadAsAsync<StartGameView>().Result;

                return PartialView("_Play", model.Players);
            }
        }


        [ExceptionLogger]
        [HttpGet]
        public async Task<ActionResult> History()
        {
            using (var http = new HttpClient())
            {
                HttpResponseMessage response = await http.GetAsync(baseUrl+ "api/game/history");

                StartGameView model = response.Content.ReadAsAsync<StartGameView>().Result;

                return PartialView("_History", model.Players);
            }
        }

    }
}