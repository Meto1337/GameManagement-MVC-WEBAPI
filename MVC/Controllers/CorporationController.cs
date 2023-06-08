using MVC.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MVC.ViewModels.Corporation;
using Data.Entities;

namespace MVC.Controllers
{
    public class CorporationController : Controller
    {
        private readonly Uri _url = new Uri("https://localhost:44336/api/corporation");

        // GET: Corporation
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
                    JsonConvert.DeserializeObject<List<CorporationVM>>(jsonString);



                model.Pager = model.Pager ?? new PagerVM();

                model.Pager.Page = model.Pager.Page <= 0
                                            ? 1
                                            : model.Pager.Page;

                model.Pager.ItemsPerPage = model.Pager.ItemsPerPage <= 0
                                            ? 12
                                            : model.Pager.ItemsPerPage;


                model.Filter = model.Filter ?? new FilterVM();


                model.Pager.PagesCount = (int)Math.Ceiling(responseData.Where(u =>
                string.IsNullOrEmpty(model.Filter.CorporationName) || u.CorporationName.Contains(model.Filter.CorporationName)).Count() / (double)model.Pager.ItemsPerPage);


                model.Items = responseData
                                        .OrderBy(i => i.Id)
                                        .Where(u =>
                                               string.IsNullOrEmpty(model.Filter.CorporationName) || u.CorporationName.Contains(model.Filter.CorporationName))
                                        .Skip(model.Pager.ItemsPerPage * (model.Pager.Page - 1))
                                        .Take(model.Pager.ItemsPerPage)
                                        .ToList();

                return View(model);

            }
        }

        public async Task<ActionResult> Details(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // make the request
                HttpResponseMessage response = await client.GetAsync("corporation/" + id);

                // parse the response and return data
                string jsonString = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<CorporationVM>(jsonString);
                return View(responseData);
            }
        }

        // api/corporation/create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CorporationVM corporationVM)
        {
            try
            {
                HttpResponseMessage response;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = _url;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var content = JsonConvert.SerializeObject(corporationVM);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    // Make the request
                    response = await client.PostAsync("", byteContent);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // api/corporation/edit/id
        public async Task<ActionResult> Edit(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // make the request
                HttpResponseMessage response = await client.GetAsync("corporation/" + id);

                // parse the response and return data
                string jsonString = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<CorporationVM>(jsonString);
                return View(responseData);
            }
        }
        

        [HttpPost]
        public async Task<ActionResult> Edit(CorporationVM corporationVM)
        {
            try
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = _url;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var content = JsonConvert.SerializeObject(corporationVM);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    // make the request // Save or Update?
                    HttpResponseMessage response = await client.PutAsync("", byteContent);

                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // api/corporation/id
        public async Task<ActionResult> Delete(int id)
        {
          
            HttpResponseMessage response;
            using (var client = new HttpClient())
            {
                client.BaseAddress = _url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Make the request
                response = await client.DeleteAsync("corporation/" + id);

                return RedirectToAction("Index");
            }
        }
    }
}
