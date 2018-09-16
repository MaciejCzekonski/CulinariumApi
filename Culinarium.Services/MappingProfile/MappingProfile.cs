using AutoMapper;
using Culinarium.Data.DbModels;
using Culinarium.Data.Dtos;
using Culinarium.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Culinarium.Services.MappingProfile
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Recipe, RecipeDto>()
                .ForMember(x => x.NumberOfRatings, opt => opt.MapFrom(x => x.Ratings.Count))
                .ForMember(x => x.AvgRating, opt => opt.MapFrom(x => x.Ratings.Count > 0 ? (int)(x.Ratings.Where(r => r.Value > 0).Average(r => r.Value) + 0.15) : 0.0));
            CreateMap<Recipe, RecipeChildDto>();
            CreateMap<Picture, PictureDto>();
            CreateMap<Picture, PictureChildDto>();
            CreateMap<Rating, RecipeRatingDto>();
            CreateMap<Ingredient, RecipeIngredientDto>();

            CreateMap<RecipeViewModel, Recipe>();
            CreateMap<UpdateRecipeViewModel, Recipe>();
            CreateMap<RatingViewModel, Rating>();
            CreateMap<IngredientViewModel, Ingredient>();
        }
    }
}
