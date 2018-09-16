using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Culinarium.Data.DbModels
{
    public class Recipe : Entity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int PreparationTime { get; set; }
        public int DifficultyRating { get; set; }
        public Picture Picture { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; }
        public ICollection<Rating> Ratings { get; set; }
    }
}
