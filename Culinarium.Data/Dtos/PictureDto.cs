using System;
using System.Collections.Generic;
using System.Text;

namespace Culinarium.Data.Dtos
{
    public class PictureDto : BaseDto
    {
        public string FullResolutionPicName { get; set; }
        public string MediumResolutionPicName { get; set; }
        public string SmallResolutionPicName { get; set; }

        public RecipeChildDto Recipe { get; set; }
    }
}
