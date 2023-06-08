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
using MVC.ViewModels.Developer;
using Data.Entities;

namespace MVC.Controllers
{
    public class DeveloperController : Controller
    {
        private readonly Uri _url = new Uri("https://localhost:44336/api/developer");

        // GET: developer
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
                    JsonConvert.DeserializeObject<List<DeveloperVM>>(jsonString);



                model.Pager = model.Pager ?? new PagerVM();

                model.Pager.Page = model.Pager.Page <= 0
                                            ? 1
                                            : model.Pager.Page;

                model.Pager.ItemsPerPage = model.Pager.ItemsPerPage <= 0
                                            ? 12
                                            : model.Pager.ItemsPerPage;


                model.Filter = model.Filter ?? new FilterVM();


                model.Pager.PagesCount = (int)Math.Ceiling(responseData.Where(u =>
                string.IsNullOrEmpty(model.Filter.LastName) || u.LastName.Contains(model.Filter.LastName)).Count() / (double)model.Pager.ItemsPerPage);


                model.Items = responseData
                                        .OrderBy(i => i.Id)
                                        .Where(u =>
                                               string.IsNullOrEmpty(model.Filter.LastName) || u.LastName.Contains(model.Filter.LastName))
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
                HttpResponseMessage response = await client.GetAsync("developer/" + id);

                // parse the response and return data
                string jsonString = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<DeveloperVM>(jsonString);
                return View(responseData);
            }
        }


        // api/developer/create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(DeveloperVM developerVM)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = _url;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var content = JsonConvert.SerializeObject(developerVM);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    // Make the request
                    HttpResponseMessage response = await client.PostAsync("", byteContent);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // api/developer/edit/id
        public async Task<ActionResult> Edit(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Make the request
                HttpResponseMessage response = await client.GetAsync("developer/" + id);

                // Parse the response and return data
                string jsonString = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<DeveloperVM>(jsonString);
                return View(responseData);
            }
        }


        [HttpPost]
        public async Task<ActionResult> Edit(DeveloperVM developerVM)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = _url;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var content = JsonConvert.SerializeObject(developerVM);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                  
                    HttpResponseMessage response = await client.PutAsync("", byteContent);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // api/developer/id
        public async Task<ActionResult> Delete(int id)
        {
            // string accessToken = await GetAccessToken();

            using (var client = new HttpClient())
            {
                client.BaseAddress = _url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Make the request
                HttpResponseMessage response = await client.DeleteAsync("developer/" + id);

                return RedirectToAction("Index");
            }
        }
    }
}
