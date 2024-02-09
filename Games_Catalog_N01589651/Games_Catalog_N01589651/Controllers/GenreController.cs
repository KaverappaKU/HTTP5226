using Games_Catalog_N01589651.Models;
using Games_Catalog_N01589651.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Games_Catalog_N01589651.Controllers
{
    public class GenreController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static GenreController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44370/api/");
        }

        // GET: Genre/List
        public ActionResult List()
        {
            //objective: communicate with our Genre data api to retrieve a list of games
            //curl https://localhost:44370/api/genredata/listgenre


            string url = "genredata/listgenre";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<GenreDto> genre = response.Content.ReadAsAsync<IEnumerable<GenreDto>>().Result;
            return View(genre);
        }

        // GET: Genre/Details/2
        public ActionResult Details(int id)
        {
            GenreDetails ViewModel = new GenreDetails();
            //objective: communicate with our Genre data api to retrieve one Genre
            //curl https://localhost:44370/api/genredata/findgenre/{id}

            string url = "genredata/findgenre/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            GenreDto SelectedGenre = response.Content.ReadAsAsync<GenreDto>().Result;

            url = "GameData/ListGamesForGenre/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<GameDto> gameDtos = response.Content.ReadAsAsync<IEnumerable<GameDto>>().Result;



            ViewModel.SelectedGenre = SelectedGenre;
            ViewModel.GameCollection = gameDtos;

            return View(ViewModel);
        }

        public ActionResult Error()
        {

            return View();
        }

        // GET: Genre/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Genre/Create
        [HttpPost]
        public ActionResult Create(Genre Genre)
        {
            Debug.WriteLine("the json payload is :");
            //objective: add a new Genre into our system using the API
            //curl -H "Content-Type:application/json" -d @Genre.json https://localhost:44370/api/genredata/addgenre
            string url = "genredata/addgenre";


            string jsonpayload = jss.Serialize(Genre);
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

        // GET: Genre/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "genredata/findgenre/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            GenreDto SelectedGenre = response.Content.ReadAsAsync<GenreDto>().Result;

            return View(SelectedGenre);
        }

        // POST: Genre/Update/5
        [HttpPost]
        public ActionResult Update(int id, Genre Genre)
        {

            string url = "genredata/updategenre/" + id;
            string jsonpayload = jss.Serialize(Genre);
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

        // GET: Genre/Delete/3
        public ActionResult DeleteConfirm(int id)
        {
            string url = "genredata/findgenre/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            GenreDto SelectedGenre = response.Content.ReadAsAsync<GenreDto>().Result;
            return View(SelectedGenre);
        }

        // POST: Genre/Delete/3
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "genredata/deletegenre/" + id;
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