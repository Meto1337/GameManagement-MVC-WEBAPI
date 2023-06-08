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
    public class CorporationController : ApiController
    {
        private CorporationManagementService corporationService = new CorporationManagementService();

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Json(corporationService.Get());
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            ResponseMessage response = new ResponseMessage();
            if (!(corporationService.GetById(id).Validate()))
            {
                response.Code = 500;
                response.Body = $"Corporation with ID:{id} is not found.";
                return Json(response);
            }
            return Json(corporationService.GetById(id));
        }

        [HttpPut]
        public IHttpActionResult Update(CorporationDTO corporationDTO)
        {
            ResponseMessage response = new ResponseMessage();

            if (corporationService.Update(corporationDTO))
            {
                response.Code = 200;
                response.Body = "Corporation is updated.";
            }
            else
            {
                response.Code = 500;
                response.Body = "Corporation is not updated.";
            }

            return Json(response);
        }


        [HttpPost]
        public IHttpActionResult Save(CorporationDTO corporationDTO)
        { 
            ResponseMessage response = new ResponseMessage();

            if (corporationService.Save(corporationDTO))
            {
                response.Code = 200;
                response.Body = "Corporation is saved.";
            }
            else
            {
                response.Code = 500;
                response.Body = "Corporation is not saved.";
            }

            return Json(response);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            ResponseMessage response = new ResponseMessage();

            if (corporationService.Delete(id))
            {
                response.Code = 200;
                response.Body = "Corporation is deleted.";
            }
            else
            {
                response.Code = 500;
                response.Body = "Corporation is not deleted.";
            }

            return Json(response);
        }
    }
}
