using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Culinarium.Data.ViewModels
{
    public class RatingViewModel
    {
        [Required]
        public int Value { get; set; }
        [Required]
        public int RecipeId { get; set; }
    }
}
