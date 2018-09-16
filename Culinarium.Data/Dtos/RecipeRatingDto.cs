using System;
using System.Collections.Generic;
using System.Text;

namespace Culinarium.Data.Dtos
{
    public class RecipeRatingDto : BaseDto
    {
        public int Value { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
