﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Culinarium.Data.ViewModels
{
    public class IngredientViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int RecipeId { get; set; }
    }
}
