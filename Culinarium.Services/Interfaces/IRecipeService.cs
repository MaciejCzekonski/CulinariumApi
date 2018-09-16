using Culinarium.Data.DbModels;
using Culinarium.Data.Dtos;
using Culinarium.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Culinarium.Services.Interfaces
{
    public interface IRecipeService
    {
        IEnumerable<RecipeDto> GetAll(int option);
        RecipeDto GetBy(Expression<Func<Recipe, bool>> getBy);
        ResultDto<RecipeDto> Insert(RecipeViewModel recipeViewModel);
        ResultDto<RecipeDto> Update(UpdateRecipeViewModel recipeViewModel);
        IEnumerable<RecipeDto> Search(string query);
        ResultDto<BaseDto> Delete(int recipeId);
    }
}
