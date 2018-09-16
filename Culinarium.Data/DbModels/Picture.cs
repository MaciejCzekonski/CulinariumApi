using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Culinarium.Data.DbModels
{
    public class Picture : Entity
    {
        [Required]
        public string FullResolutionPicName { get; set; }
        [Required]
        public string MediumResolutionPicName { get; set; }
        [Required]
        public string SmallResolutionPicName { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
