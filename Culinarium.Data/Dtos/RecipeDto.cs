using System;
using System.Collections.Generic;
using System.Text;

namespace Culinarium.Data.Dtos
{
    public class RecipeDto : BaseDto
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public int PreparationTime { get; set; }
        public int DifficultyRating { get; set; }
        public double AvgRating { get; set; }
        public int NumberOfRatings { get; set; }
        public PictureDto Picture { get; set; }
        public ICollection<RecipeIngredientDto> Ingredients { get; set; }
    }
}
