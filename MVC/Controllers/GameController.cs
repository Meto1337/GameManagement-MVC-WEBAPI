using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Data.Context;
using Data.Entities;
using Newtonsoft.Json;
using MVC.ViewModels.Game;
using MVC.ViewModels;

namespace MVC.Controllers
{
    public class GameController : Controller
    {
        private readonly Uri _url = new Uri("https://localhost:44336/api/game");

        // GET: Game
        public async Task<ActionResult> Index(IndexVM model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders
                    .Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //make the request
                HttpResponseMessage response = await client.GetAsync("");

                //parse the response and return data
                string jsonString = await response.Content.ReadAsStringAsync();
                var responseData =
                    JsonConvert.DeserializeObject<List<GameVM>>(jsonString);


                model.Pager = model.Pager ?? new PagerVM();

                model.Pager.Page = model.Pager.Page <= 0
                                            ? 1
                                            : model.Pager.Page;

                model.Pager.ItemsPerPage = model.Pager.ItemsPerPage <= 0
                                            ? 12
                                            : model.Pager.ItemsPerPage;


                model.Filter = model.Filter ?? new FilterVM();


                model.Pager.PagesCount = (int)Math.Ceiling(responseData.Where(u =>
                string.IsNullOrEmpty(model.Filter.Title) || u.Title.Contains(model.Filter.Title)).Count() / (double)model.Pager.ItemsPerPage);


                model.Items = responseData
                                        .OrderBy(i => i.Id)
                                        .Where(u =>
                                               string.IsNullOrEmpty(model.Filter.Title) || u.Title.Contains(model.Filter.Title))
                                        .Skip(model.Pager.ItemsPerPage * (model.Pager.Page - 1))
                                        .Take(model.Pager.ItemsPerPage)
                                        .ToList();


                return View(model);

            }
        }

        public async Task<ActionResult> Details(int id)
        {
            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                client.BaseAddress = _url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // make the request
                 response = await client.GetAsync("game/" + id);

                // parse the response and return data
                string jsonString = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<GameVM>(jsonString);
                return View(responseData);
            }
        }

        // api/game/create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(GameVM gameVM)
        {
            try
            {
                HttpResponseMessage response;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = _url;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var content = JsonConvert.SerializeObject(gameVM);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    // make the request
                    response = await client.PostAsync("", byteContent);


                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // api/game/edit/id
        public async Task<ActionResult> Edit(int id)
        {
            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                client.BaseAddress = _url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // make the request
                response = await client.GetAsync("game/" + id);

                // parse the response and return data
                string jsonString = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<GameVM>(jsonString);
                return View(responseData);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(GameVM gameVM)
        {
            try
            {
                HttpResponseMessage response;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = _url;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var content = JsonConvert.SerializeObject(gameVM);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    // make the request // Save or Update?
                    response = await client.PutAsync("", byteContent);

                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // api/game/id
        public async Task<ActionResult> Delete(int id)
        {

            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                client.BaseAddress = _url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // make the request
                response = await client.DeleteAsync("game/" + id);

                return RedirectToAction("Index");
            }
        }
    }
}
