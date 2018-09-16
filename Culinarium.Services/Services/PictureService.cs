using Culinarium.Data.DbModels;
using Culinarium.Data.Dtos;
using Culinarium.Repository.Interfaces;
using Culinarium.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Transforms;
using SixLabors.Primitives;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Culinarium.Services.Services
{
    public class PictureService : IPictureService
    {
        private readonly IRepository<Recipe> _recipeRepository;
        private readonly IRepository<Picture> _pictureRepository;
        private readonly IMapperManager _mapper;
        private readonly IHostingEnvironment _env;

        public PictureService(IRepository<Recipe> recipeRepository, IRepository<Picture> pictureRepository, IMapperManager mapper, IHostingEnvironment env)
        {
            _recipeRepository = recipeRepository;
            _pictureRepository = pictureRepository;
            _mapper = mapper;
            _env = env;
        }

        public ResultDto<PictureDto> AddRecipePicture(IFormFile pictureFile, int recipeId)
        {
            var result = new ResultDto<PictureDto>();
            if(pictureFile == null)
            {
                result.Errors.Add(_Errors.PictureFileNotExist);
                return result;
            }
            if(pictureFile.Length > 5242880)
            {
                result.Errors.Add(_Errors.PictureTooBig);
            }

            var recipe = _recipeRepository.GetBy(x => x.Id == recipeId, x => x.Picture);
            if (recipe == null)
            {
                result.Errors.Add(_Errors.RecipeDoesNotExist);
            }
            if (result.IsError)
            {
                return result;
            }

            if(recipe.Picture != null)
            {
                DeletePictureFiles(recipe.Picture, _env.WebRootPath);
                _pictureRepository.Delete(x => x.Id == recipe.Picture.Id);
            }

            var picture = SavePictureFiles(pictureFile);
            picture.Recipe = recipe;
            if(_pictureRepository.Insert(picture) == 0)
            {
                result.Errors.Add(_Errors.SaveFail);
                return result;
            }

            result.SuccessResult = _mapper.Map<PictureDto>(picture);
            return result;
        }

        private void DeletePictureFiles(Picture picture, string webRootPath)
        {
            DeleteExistsFile(picture.FullResolutionPicName, webRootPath);
            DeleteExistsFile(picture.MediumResolutionPicName, webRootPath);
            DeleteExistsFile(picture.SmallResolutionPicName, webRootPath);
        }

        private void DeleteExistsFile(string filename, string webRootPath)
        {
            var path = Path.Combine(webRootPath, "pictures", filename);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        private Picture SavePictureFiles(IFormFile pictureFile)
        {
            var picture = new Picture();

            var Ffilename = Path.GetRandomFileName() + DateTime.Now.ToString("yyMMddhhmmss");
            var MfileName = Ffilename + "_m.jpg";
            var Sfilename = Ffilename + "_s.jpg";
            Ffilename += "_f.jpg";

            var srcPath = Path.GetTempFileName();
            using(var stream = new FileStream(srcPath, FileMode.Open))
            {
                pictureFile.CopyTo(stream);
            }
            var destPath = Path.Combine(_env.WebRootPath, "pictures", Ffilename);
            ResizeAndSave(srcPath, destPath, 500);
            picture.FullResolutionPicName = Ffilename;

            srcPath = destPath;
            destPath = Path.Combine(_env.WebRootPath, "pictures", MfileName);
            ResizeAndSave(srcPath, destPath, 300);
            picture.MediumResolutionPicName = MfileName;

            srcPath = destPath;
            destPath = Path.Combine(_env.WebRootPath, "pictures", Sfilename);
            ResizeAndSave(srcPath, destPath, 250);
            picture.SmallResolutionPicName = Sfilename;

            return picture;
        }

        private bool ResizeAndSave(string srcPath, string destPath, int maxRes)
        {
            bool resized = false;
            using (Image<Rgba32> image = Image.Load(srcPath))
            {
                double scale;
                var bigger = image.Width;
                if (bigger > maxRes)
                {
                    scale = (double)maxRes / bigger;
                    image.Mutate(x => x.Resize(new Size((int)(image.Width * scale + 0.5), (int)(image.Height * scale + 0.5))));
                    resized = true;
                }
                image.Save(destPath);
                return resized;
            }
        }

    }
}
