using Culinarium.Data.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culinarium.Services.Interfaces
{
    public interface IPictureService
    {
        ResultDto<PictureDto> AddRecipePicture(IFormFile pictureFile, int recipeId);
    }
}
