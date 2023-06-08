using ApplicationService.DTOs;
using ApplicationService.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Messages;

namespace WEBAPI.Controllers
{
    public class DeveloperController : ApiController
    {
        private DeveloperManagementService developerService = new DeveloperManagementService();

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Json(developerService.Get());
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            ResponseMessage response = new ResponseMessage();
            if (!(developerService.GetById(id).Validate()))
            {
                response.Code = 500;
                response.Body = $"Developer with ID:{id} is not found.";
                return Json(response);
            }
            return Json(developerService.GetById(id));
        }

        [HttpPut]
        public IHttpActionResult Update(DeveloperDTO developerDTO)
        {
            ResponseMessage response = new ResponseMessage();

            if (developerService.Update(developerDTO))
            {
                response.Code = 200;
                response.Body = "Developer is updated.";
            }
            else
            {
                response.Code = 500;
                response.Body = "Developer is not updated.";
            }

            return Json(response);
        }


        [HttpPost]
        public IHttpActionResult Save(DeveloperDTO developerDTO)
        {
            ResponseMessage response = new ResponseMessage();

            if (developerService.Save(developerDTO))
            {
                response.Code = 200;
                response.Body = "Developer is saved.";
            }
            else
            {
                response.Code = 500;
                response.Body = "Developer is not saved.";
            }

            return Json(response);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            ResponseMessage response = new ResponseMessage();

            if (developerService.Delete(id))
            {
                response.Code = 200;
                response.Body = "Developer is deleted.";
            }
            else
            {
                response.Code = 500;
                response.Body = "Developer is not deleted.";
            }

            return Json(response);
        }
    }
}
