using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Culinarium.Data.ViewModels
{
    public class RecipeViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public int PreparationTime { get; set; }
        public int DifficultyRating { get; set; }
    }
}
