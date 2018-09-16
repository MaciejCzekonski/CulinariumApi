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
    public class RatingService : IRatingService
    {
        private readonly IRepository<Recipe> _recipeRepository;
        private readonly IRepository<Rating> _ratingRepository;
        private readonly IMapperManager _mapper;

        public RatingService(IRepository<Recipe> recipeRepository, IRepository<Rating> ratingRepository, IMapperManager mapper)
        {
            _recipeRepository = recipeRepository;
            _ratingRepository = ratingRepository;
            _mapper = mapper;
        }

        public ResultDto<BaseDto> InsertRating(RatingViewModel ratingViewModel)
        {
            var result = new ResultDto<BaseDto>();
            if (!_recipeRepository.Exist(x => x.Id == ratingViewModel.RecipeId))
            {
                result.Errors.Add(_Errors.RecipeDoesNotExist);
            }
            if (result.IsError)
            {
                return result;
            }

            var rating = _mapper.Map<Rating>(ratingViewModel);
            if(_ratingRepository.Insert(rating) == 0)
            {
                result.Errors.Add(_Errors.SaveFail);
                return result;
            }

            return result;
        }
    }
}
