using Culinarium.Data.ViewModels;
using Culinarium.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Culinarium.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PictureController : Controller
    {
        private IPictureService _pictureService;

        public PictureController(IPictureService pictureService)
        {
            _pictureService = pictureService;
        }

        [HttpPost("addrecipepicture")]
        public IActionResult AddPicture(IFormFile picture, int recipeId)
        {
            if(picture == null || recipeId == 0)
            {
                return BadRequest();
            }

            var result = _pictureService.AddRecipePicture(picture, recipeId);
            if (result.IsError)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
