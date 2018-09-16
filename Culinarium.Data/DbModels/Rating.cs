using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Culinarium.Data.DbModels
{
    public class Rating : Entity
    {
        [Required]
        public int Value { get; set; }
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
