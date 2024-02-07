using Games_Catalog_N01589651.Models;
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
    public class DeveloperController : Controller
    {
        // GET: Developer
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static DeveloperController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44370/api/");
        }

        // GET: Developer/List
        public ActionResult List()
        {
            //objective: communicate with our Developer data api to retrieve a list of games
            //curl https://localhost:44370/api/developerdata/listdeveloper


            string url = "developerdata/listdeveloper";
            HttpResponseMessage response = client.GetAsync(url).Result;

            IEnumerable<DeveloperDto> Developer = response.Content.ReadAsAsync<IEnumerable<DeveloperDto>>().Result;
            return View(Developer);
        }

        // GET: Developer/Details/2
        public ActionResult Details(int id)
        {

            //objective: communicate with our Developer data api to retrieve one Developer
            //curl https://localhost:44370/api/developerdata/finddeveloper/{id}

            string url = "developerdata/finddeveloper/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            DeveloperDto SelectedDeveloper = response.Content.ReadAsAsync<DeveloperDto>().Result;
            return View(SelectedDeveloper);
        }

        public ActionResult Error()
        {

            return View();
        }

        // GET: Developer/New
        public ActionResult New()
        {
            //information about all Developer in the system.
            //GET api/developerdata/listdeveloper

            //string url = "developerdata/listdeveloper";
            //HttpResponseMessage response = client.GetAsync(url).Result;
            //IEnumerable<DeveloperDto> SpeciesOptions = response.Content.ReadAsAsync<IEnumerable<DeveloperDto>>().Result;

            //return View(GenreOptions);
            return View();
        }

        // POST: Developer/Create
        [HttpPost]
        public ActionResult Create(Developer Developer)
        {
            Debug.WriteLine("the json payload is :");
            //objective: add a new Developer into our system using the API
            //curl -H "Content-Type:application/json" -d @Developer.json https://localhost:44370/api/developerdata/adddeveloper
            string url = "developerdata/adddeveloper";


            string jsonpayload = jss.Serialize(Developer);
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

        // GET: Developer/Edit/5
        public ActionResult Edit(int id)
        {
            //updatedeveloper ViewModel = new updatedeveloper();

            ////the existing Developer information
            //string url = "developerdata/finddeveloper/" + id;
            //HttpResponseMessage response = client.GetAsync(url).Result;
            //DeveloperDto SelectedDeveloper = response.Content.ReadAsAsync<DeveloperDto>().Result;
            //ViewModel.SelectedDeveloper = SelectedDeveloper;

            //// all Developer to choose from when updating this Developer
            ////the existing Developer information
            //url = "developerdata/listdeveloper/";
            //response = client.GetAsync(url).Result;
            //IEnumerable<DeveloperDto> GenreOptions = response.Content.ReadAsAsync<IEnumerable<DeveloperDto>>().Result;

            //ViewModel.GenreOptions = GenreOptions;

            //return View(ViewModel);
            string url = "developerdata/finddeveloper/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            //Debug.WriteLine("The response code is ");
            //Debug.WriteLine(response.StatusCode);

            DeveloperDto SelectedDeveloper = response.Content.ReadAsAsync<DeveloperDto>().Result;
            //Debug.WriteLine("Developer received : ");
            //Debug.WriteLine(selectedanimal.AnimalName);

            return View(SelectedDeveloper);
        }

        // POST: Developer/Update/5
        [HttpPost]
        public ActionResult Update(int id, Developer Developer)
        {

            string url = "developerdata/updatedeveloper/" + id;
            string jsonpayload = jss.Serialize(Developer);
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

        // GET: Developer/Delete/3
        public ActionResult DeleteConfirm(int id)
        {
            string url = "developerdata/finddeveloper/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            DeveloperDto SelectedDeveloper = response.Content.ReadAsAsync<DeveloperDto>().Result;
            return View(SelectedDeveloper);
        }

        // POST: Developer/Delete/3
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string url = "developerdata/deletedeveloper/" + id;
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