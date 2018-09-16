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
    public class IngredientController : Controller
    {
        private IIngredientService _ingredientService;

        public IngredientController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        [HttpPost("add")]
        public IActionResult AddIngredient([FromBody]IngredientViewModel ingredientViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _ingredientService.Insert(ingredientViewModel);
            if (result.IsError)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("update")]
        public IActionResult UpdateIngredient([FromBody]UpdateIngredientViewModel ingredientViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _ingredientService.Update(ingredientViewModel);
            if (result.IsError)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteIngredient(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var result = _ingredientService.Delete(id);
            if (result.IsError)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

    }
}
