using Snacks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snacks.Services.Images
{
    public interface IImagesManagement
    {
        string GetImage(int snackId, ImageType imageType);
    }
}
