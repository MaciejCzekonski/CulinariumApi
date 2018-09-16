using Culinarium.Data.DbModels;
using Culinarium.Data.Dtos;
using Culinarium.Data.ViewModels;
using Culinarium.Repository.Interfaces;
using Culinarium.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Culinarium.Services.Services
{
    public class RecipeService : IRecipeService
    {
        public readonly IRepository<Recipe> _recipeRepository;
        public readonly IMapperManager _mapper;

        public RecipeService(IRepository<Recipe> recipeRepository, IMapperManager mapper)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
        }

        public IEnumerable<RecipeDto> GetAll(int option)
        {
            var recipes = _recipeRepository.GetAll(x=>x.Ratings);
            if(recipes == null)
            {
                return null;
            }

            if (option == 0)
            {
                recipes = recipes.OrderByDescending(x => x.Ratings.Where(r => r.Value > 0).Count() > 0 ? x.Ratings.Where(r => r.Value > 0).Average(r => r.Value) : 0.0);
            }

            var recipesDto = _mapper.Map<IEnumerable<Recipe>, IEnumerable<RecipeDto>>(recipes);
            if (option == 1) 
            {
                recipesDto = recipesDto.OrderByDescending(x => x.DifficultyRating);
            }
            if (option == 2) 
            {
                recipesDto = recipesDto.OrderByDescending(x => x.PreparationTime);
            }
            return recipesDto;
        }

        public RecipeDto GetBy(Expression<Func<Recipe, bool>> getBy)
        {
            var recipe = _recipeRepository.GetBy(getBy, x => x.Ratings);
            if (recipe == null)
            {
                return null;
            }
            _recipeRepository.LoadRelatedCollection(recipe, x => x.Ratings);
            _recipeRepository.LoadRelatedCollection(recipe, x => x.Ingredients);

            var recipeDto = _mapper.Map<RecipeDto>(recipe);
            return recipeDto;
        }

        private string replacePolishChars(string txt)
        {
            txt = txt.ToLower();
            var polishChars = new[] { 'ą', 'ć', 'ę', 'ł', 'ń', 'ó', 'ś', 'ż', 'ź' };
            var destChars = new[] { 'a', 'c', 'e', 'l', 'n', 'o', 's', 'z', 'z' };

            for (int i = 0; i < polishChars.Length; i++) 
            {
                txt = txt.Replace(polishChars[i], destChars[i]);
            }
            return txt;
        }

        public IEnumerable<RecipeDto> Search(string query)
        {
            query = replacePolishChars(query);
            var recipes = _recipeRepository.GetAllBy(x => replacePolishChars(x.Name).Contains(query), x => x.Ratings).ToList();
            if(recipes == null)
            {
                return null;
            }
            int limit = 20;
            recipes = recipes.OrderByDescending(x => x.Ratings.Count > 0 ? x.Ratings.Average(r => r.Value) : 0.0).Take(limit > recipes.Count ? recipes.Count : limit).ToList();
            var recipesDto = _mapper.Map<IEnumerable<Recipe>, IEnumerable<RecipeDto>>(recipes);
            return recipesDto;
        }

        public ResultDto<RecipeDto> Insert(RecipeViewModel recipeViewModel)
        {
            var result = new ResultDto<RecipeDto>();
            if(_recipeRepository.Exist(x=>x.Name.ToLower() == recipeViewModel.Name.ToLower() && x.Author.ToLower() == recipeViewModel.Author.ToLower()))
            {
                result.Errors.Add(_Errors.RecipeDoesNotExist);
            }
            if (result.IsError)
            {
                return result;
            }
            var recipe = _mapper.Map<Recipe>(recipeViewModel);
            if(_recipeRepository.Insert(recipe) == 0)
            {
                result.Errors.Add(_Errors.SaveFail);
                return result;
            }
            result.SuccessResult = _mapper.Map<RecipeDto>(recipe);

            return result;
        }

        public ResultDto<RecipeDto> Update(UpdateRecipeViewModel recipeViewModel)
        {
            var result = new ResultDto<RecipeDto>();
            if(!_recipeRepository.Exist(x=>x.Id == recipeViewModel.id))
            {
                result.Errors.Add(_Errors.RecipeDoesNotExist);
            }
            if (result.IsError)
            {
                return result;
            }
            var recipe = _mapper.Map<Recipe>(recipeViewModel);
            if(_recipeRepository.Update(recipe) == 0)
            {
                result.Errors.Add(_Errors.SaveFail);
                return result;
            }
            result.SuccessResult = _mapper.Map<RecipeDto>(recipe);

            return result;
        }

        public ResultDto<BaseDto> Delete(int recipeId)
        {
            var result = new ResultDto<BaseDto>();
            if(!_recipeRepository.Exist(x=>x.Id == recipeId))
            {
                result.Errors.Add(_Errors.RecipeDoesNotExist);
                return result;
            }
            if (_recipeRepository.Delete(x => x.Id == recipeId) == 0)
            {
                result.Errors.Add(_Errors.DeleteFail);
                return result;
            }
            return result;
        }
    }
}
