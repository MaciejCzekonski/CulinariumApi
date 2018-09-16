using Culinarium.Data.ViewModels;
using Culinarium.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Culinarium.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class RatingController : Controller
    {
        private IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpPost("rate")]
        public IActionResult Rate([FromBody] RatingViewModel ratingViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _ratingService.InsertRating(ratingViewModel);
            if (result.IsError)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
