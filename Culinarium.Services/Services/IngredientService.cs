using Culinarium.Data.DbModels;
using Culinarium.Data.Dtos;
using Culinarium.Data.ViewModels;
using Culinarium.Repository.Interfaces;
using Culinarium.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culinarium.Services.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IRepository<Recipe> _recipeRepository;
        private readonly IRepository<Ingredient> _ingredientRepository;
        private readonly IMapperManager _mapper;

        public IngredientService(IRepository<Recipe> recipeRepository, IRepository<Ingredient> ingredientRepository, IMapperManager mapper)
        {
            _recipeRepository = recipeRepository;
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
        }

        public ResultDto<RecipeDto> Insert(IngredientViewModel ingredientViewModel)
        {
            var result = new ResultDto<RecipeDto>();

            if (!_recipeRepository.Exist(x => x.Id == ingredientViewModel.RecipeId))
            {
                result.Errors.Add(_Errors.RecipeDoesNotExist);
            }
            if (result.IsError)
            {
                return result;
            }
            var ingredient = _mapper.Map<Ingredient>(ingredientViewModel);

            if (_ingredientRepository.Insert(ingredient) == 0)
            {
                result.Errors.Add(_Errors.SaveFail);
                return result;
            }

            var recipe = _recipeRepository.GetBy(x => x.Id == ingredient.RecipeId, x => x.Picture);

            if (recipe == null)
            {
                result.Errors.Add(_Errors.RecipeDoesNotExist);
                return result;
            }

            result.SuccessResult = _mapper.Map<RecipeDto>(recipe);

            return result;
        }

        public ResultDto<IngredientDto> Update(UpdateIngredientViewModel ingredientViewModel)
        {
            var result = new ResultDto<IngredientDto>();
            if (!_ingredientRepository.Exist(x => x.Id == ingredientViewModel.Id))
            {
                result.Errors.Add(_Errors.IngredientDoesNotExist);
            }
            if(!_recipeRepository.Exist(x => x.Id == ingredientViewModel.RecipeId))
            {
                result.Errors.Add(_Errors.RecipeDoesNotExist);
            }
            if (result.IsError)
            {
                return result;
            }
            var ingredient = _mapper.Map<Ingredient>(ingredientViewModel);
            if (_ingredientRepository.Update(ingredient) == 0)
            {
                result.Errors.Add(_Errors.SaveFail);
                return result;
            }
            result.SuccessResult = _mapper.Map<IngredientDto>(ingredient);

            return result;
        }

        public ResultDto<BaseDto> Delete(int ingredientId)
        {
            var result = new ResultDto<BaseDto>();
            var ingredient = _ingredientRepository.GetBy(x => x.Id == ingredientId);
            if(ingredient == null)
            {
                result.Errors.Add(_Errors.IngredientDoesNotExist);
            }
            if (result.IsError)
            {
                return result;
            }
            if(_ingredientRepository.Delete(x=>x.Id == ingredientId) == 0)
            {
                result.Errors.Add(_Errors.DeleteFail);
                return result;
            }

            return result;
        }
    }
}
