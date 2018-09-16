using Culinarium.Data.Dtos;
using Culinarium.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culinarium.Services.Interfaces
{
    public interface IRatingService
    {
        ResultDto<BaseDto> InsertRating(RatingViewModel ratingViewModel);
    }
}
