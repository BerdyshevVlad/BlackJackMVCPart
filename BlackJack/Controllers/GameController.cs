using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Web.Mvc;
using BlackJack.ExceptionLoggers;
using BlackJack.ViewModels;
using Newtonsoft.Json;

namespace BlackJack.Controllers
{
    public class GameController : Controller
    {
        private const string BASE_URL = "http://localhost:50610/api/game";


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
            try
            {
                using (var http = new HttpClient())
                {
                    string json = JsonConvert.SerializeObject(userNameAndBotCount);
                    HttpContent content = new StringContent(json);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseMessage response = await http.PostAsync(BASE_URL + "/start", content);

                    StartGameView model = response.Content.ReadAsAsync<StartGameView>().Result;

                    if (model == null)
                    {
                        throw new Exception("Not found");
                    }

                    return PartialView("_Play", model.Players);
                }
            }
            catch (Exception e)
            {
                return View("Error", e.Message);
            }
        }


        [ExceptionLogger]
        [HttpGet]
        public async Task<ActionResult> More()
        {
            try
            {
                using (var http = new HttpClient())
                {
                    HttpResponseMessage response = await http.GetAsync(BASE_URL + "/more");
                    MoreGameView model = response.Content.ReadAsAsync<MoreGameView>().Result;

                    if (model == null)
                    {
                        throw new Exception("Not found");
                    }

                    return PartialView("_Play", model.Players);
                }
            }
            catch (Exception e)
            {
                return View("Error", e.Message);
            }
        }


        [ExceptionLogger]
        [HttpGet]
        public async Task<ActionResult> Enough()
        {
            try
            {
                using (var http = new HttpClient())
                {
                    HttpResponseMessage response = await http.GetAsync(BASE_URL + "/enough");
                    EnoughGameView model = response.Content.ReadAsAsync<EnoughGameView>().Result;

                    if (model == null)
                    {
                        throw new Exception("Not found");
                    }

                    return PartialView("_Play", model.Players);
                }
            }
            catch (Exception e)
            {
                return View("Error", e.Message);
            }
        }


        [ExceptionLogger]
        [HttpGet]
        public async Task<ActionResult> History()
        {
            try
            {
                using (var http = new HttpClient())
                {
                    HttpResponseMessage response = await http.GetAsync(BASE_URL + "/history");
                    HistoryGameView model = response.Content.ReadAsAsync<HistoryGameView>().Result;

                    if (model == null)
                    {
                        throw new Exception("Not found");
                    }

                    return View("History", "", JsonConvert.SerializeObject(model.Players));
                }
            }
            catch (Exception e)
            {
                return View("Error",e.Message);
            }
        }
    }
}