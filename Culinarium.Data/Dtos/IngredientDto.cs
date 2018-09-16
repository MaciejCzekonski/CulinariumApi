using System;
using System.Collections.Generic;
using System.Text;

namespace Culinarium.Data.Dtos
{
    public class IngredientDto : BaseDto
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public RecipeChildDto Recipe { get; set; }
    }
}
