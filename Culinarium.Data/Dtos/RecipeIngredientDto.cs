using System;
using System.Collections.Generic;
using System.Text;

namespace Culinarium.Data.Dtos
{
    public class RecipeIngredientDto : BaseDto
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
