using Culinarium.Data.Dtos;
using Culinarium.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culinarium.Services.Interfaces
{
    public interface IIngredientService
    {
        ResultDto<RecipeDto> Insert(IngredientViewModel ingredientViewModel);
        ResultDto<IngredientDto> Update(UpdateIngredientViewModel ingredientViewModel);
        ResultDto<BaseDto> Delete(int ingredientId);
    }
}
