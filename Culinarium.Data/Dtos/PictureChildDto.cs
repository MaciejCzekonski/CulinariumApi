using System;
using System.Collections.Generic;
using System.Text;

namespace Culinarium.Data.Dtos
{
    public class PictureChildDto : BaseDto
    {
        public string FullResolutionPicName { get; set; }
        public string MediumResolutionPicName { get; set; }
        public string SmallResolutionPicName { get; set; }
    }
}
