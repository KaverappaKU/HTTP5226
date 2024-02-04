using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Games_Catalog_N01589651.Models;

namespace Games_Catalog_N01589651.Controllers
{
    public class GameController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static GameController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44370/api/");
        }

        // GET: Game/List
        public ActionResult List()
        {
            //objective: communicate with our animal data api to retrieve a list of animals
            //curl https://localhost:44370/api/gamedata/listgames


            string url = "gamedata/listgames";
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            IEnumerable<GameDto> games = response.Content.ReadAsAsync<IEnumerable<GameDto>>().Result;
            //Debug.WriteLine("Number of animals received : ");
            //Debug.WriteLine(animals.Count());


            return View(games);
        }

        // GET: Game/Details/2
        public ActionResult Details(int id)
        {
            //objective: communicate with our animal data api to retrieve one animal
            //curl https://localhost:44370/api/gamedata/listgames/{id}

            string url = "findgame/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            GameDto selectedGame = response.Content.ReadAsAsync<GameDto>().Result;
            Debug.WriteLine(selectedGame.GameName);


            return View(selectedGame);
        }
    }
}