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
            //DetailsGames ViewModel = new DetailsGames();

            //objective: communicate with our animal data api to retrieve one game
            //curl https://localhost:44324/api/animaldata/findanimal/{id}

            string url = "gamedata/findgame/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            GameDto SelectedGame = response.Content.ReadAsAsync<GameDto>().Result;

            //ViewModel.SelectedGame = SelectedGame;

            //url = "developerdata/listdevelopersforgame/" + id;
            //response = client.GetAsync(url).Result;
            //IEnumerable<DeveloperDto> ResponsibleDevelopers = response.Content.ReadAsAsync<IEnumerable<DeveloperDto>>().Result;

            //ViewModel.ResponsibleDevelopers = ResponsibleDevelopers;

            //url = "developerdata/listdevelopersnotforgame/" + id;
            //response = client.GetAsync(url).Result;
            //IEnumerable<DeveloperDto> AvailableDevelopers = response.Content.ReadAsAsync<IEnumerable<DeveloperDto>>().Result;

            //ViewModel.AvailableDevelopers = AvailableDevelopers;


            return View(SelectedGame);
        }


        //POST: Game/Associate/{gameid}
        [HttpPost]
        public ActionResult Associate(int id, int DeveloperID)
        {
            Debug.WriteLine("Attempting to associate game :" + id + " with developer " + DeveloperID);

            //call our api to associate game with developer
            string url = "gamedata/associategamewithdeveloper/" + id + "/" + DeveloperID;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            return RedirectToAction("Details/" + id);
        }


        //Get: Game/UnAssociate/{id}?DeveloperID={developerID}
        [HttpGet]
        public ActionResult UnAssociate(int id, int DeveloperID)
        {
            Debug.WriteLine("Attempting to unassociate game :" + id + " with developer: " + DeveloperID);

            //call our api to associate game with developer
            string url = "gamedata/unassociategamewithdeveloper/" + id + "/" + DeveloperID;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            return RedirectToAction("Details/" + id);
        }


        public ActionResult Error()
        {

            return View();
        }

        // GET: Animal/New
        public ActionResult New()
        {
            //information about all genre in the system.
            //GET api/genredata/listgenre

            //string url = "genredata/listgenre";
            //HttpResponseMessage response = client.GetAsync(url).Result;
            //IEnumerable<GenreDto> SpeciesOptions = response.Content.ReadAsAsync<IEnumerable<GenreDto>>().Result;

            //return View(GenreOptions);
            return View();
        }

        // POST: Game/Create
        [HttpPost]
        public ActionResult Create(Game game)
        {
            Debug.WriteLine("the json payload is :");
            //Debug.WriteLine(animal.AnimalName);
            //objective: add a new animal into our system using the API
            //curl -H "Content-Type:application/json" -d @animal.json https://localhost:44324/api/animaldata/addanimal 
            string url = "gamedata/addgame";


            string jsonpayload = jss.Serialize(game);
            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }


        }

        // GET: Animal/Edit/5
        public ActionResult Edit(int id)
        {
            //UpdateGame ViewModel = new UpdateGame();

            ////the existing game information
            //string url = "gamedata/findgame/" + id;
            //HttpResponseMessage response = client.GetAsync(url).Result;
            //GameDto SelectedGame = response.Content.ReadAsAsync<GameDto>().Result;
            //ViewModel.SelectedGame = SelectedGame;

            //// all genre to choose from when updating this game
            ////the existing game information
            //url = "genredata/listgenre/";
            //response = client.GetAsync(url).Result;
            //IEnumerable<GenreDto> GenreOptions = response.Content.ReadAsAsync<IEnumerable<GenreDto>>().Result;

            //ViewModel.GenreOptions = GenreOptions;

            //return View(ViewModel);
            string url = "findgame/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            GameDto selectedgame = response.Content.ReadAsAsync<GameDto>().Result;
            //Debug.WriteLine("animal received : ");
            //Debug.WriteLine(selectedanimal.AnimalName);

            return View(selectedgame);
        }

        // POST: Game/Update/5
        [HttpPost]
        public ActionResult Update(int id, Game animal)
        {

            string url = "Gamedata/updategame/" + id;
            string jsonpayload = jss.Serialize(animal);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Game/Delete/3
        public ActionResult DeleteConfirm(int id)
        {
            string url = "gamedata/findgame/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            GameDto selectedgame = response.Content.ReadAsAsync<GameDto>().Result;
            return View(selectedgame);
        }

        // POST: Game/Delete/3
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "gamedata/gameanimal/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }
}