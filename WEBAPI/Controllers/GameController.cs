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
    public class GameController : ApiController
    {
        private GameManagementService gameService = new GameManagementService();
       
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Json(gameService.Get());
        }
       
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            ResponseMessage response = new ResponseMessage();

            if (!(gameService.GetById(id).Validate()))
            {
                response.Code = 500;
                response.Body = $"Game with ID:{id} is not found.";
                return Json(response);
            }

            return Json(gameService.GetById(id));
        }

        [HttpPut]
        public IHttpActionResult Update(GameDTO gameDTO)
        {
            ResponseMessage response = new ResponseMessage();

            if (gameService.Update(gameDTO))
            {
                response.Code = 200;
                response.Body = "Game is updated.";
            }
            else
            {
                response.Code = 500;
                response.Body = "Game is not updated.";
            }

            return Json(response);
        }
        [HttpPost]
        public IHttpActionResult Save(GameDTO gameDTO)
        {
            if (!gameDTO.Validate())
                return Json(new ResponseMessage { Code = 500, Error = "Data is not valid!" });
            ResponseMessage response = new ResponseMessage();

            if (gameService.Save(gameDTO))
            {
                response.Code = 200;
                response.Body = "Game is saved.";
            }
            else
            {
                response.Code = 500;
                response.Body = "Game is not saved.";
            }

            return Json(response);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            ResponseMessage response = new ResponseMessage();

            if (gameService.Delete(id))
            {
                response.Code = 200;
                response.Body = "Game is deleted.";
            }
            else
            {
                response.Code = 500;
                response.Body = "Game is not deleted.";
            }

            return Json(response);
        }
    }
}
