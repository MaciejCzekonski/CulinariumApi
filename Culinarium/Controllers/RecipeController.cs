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
    public class RecipeController : Controller
    {
        private IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet]
        public IActionResult GetAll(int option)
        {
            var result = _recipeService.GetAll(option);
            if(result == null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _recipeService.GetBy(x => x.Id == id);
            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("search/{query}")]
        public IActionResult Search(string query)
        {
            if(query.Length == 0)
            {
                return Ok(new object[] { });
            }
            var result = _recipeService.Search(query);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost("add")]
        public IActionResult Add([FromBody]RecipeViewModel recipeViewModel)
        {
            var result = _recipeService.Insert(recipeViewModel);
            if (result.IsError)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("update")]
        public IActionResult Update([FromBody]UpdateRecipeViewModel recipeViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _recipeService.Update(recipeViewModel);
            if (result.IsError)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var result = _recipeService.Delete(id);
            if (result.IsError)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
