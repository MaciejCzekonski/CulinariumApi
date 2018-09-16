using System;
using System.Collections.Generic;
using System.Text;

namespace Culinarium.Services.Services
{
    class _Errors
    {
        public readonly static string RecipeExist = "Taki przepis jest już dodany.";
        public readonly static string SaveFail = "Błąd podczas zapisu.";
        public readonly static string DeleteFail = "Błąd podczas usuwania.";
        public readonly static string RecipeDoesNotExist = "Przepis nie istnieje.";
        public readonly static string RatingExist = "Ta ocena już istnieje.";
        public readonly static string PictureFileNotExist = "Plik nie istnieje";
        public readonly static string PictureTooBig = "PLik jest zbyt duży";
        public readonly static string IngredientDoesNotExist = "Składnik nie istnieje";
    }
}
