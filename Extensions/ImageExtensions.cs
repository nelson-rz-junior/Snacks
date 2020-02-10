using Snacks.Models;
using Snacks.Services.Images;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Snacks.Extensions
{
    public static class ImageExtensions
    {
        public static void SetImages(this IEnumerable<Snack> snacks, IImagesManagement imagesManagement)
        {
            foreach (var snack in snacks)
            {
                snack.ThumbImageUrl = imagesManagement.GetImage(snack.Id, ImageType.Thumb);
                snack.StorefrontImageUrl = imagesManagement.GetImage(snack.Id, ImageType.Storefront);
                snack.DetailImageUrl = imagesManagement.GetImage(snack.Id, ImageType.Detail);
            }
        }

        public static void SetImages(this Snack snack, IImagesManagement imagesManagement)
        {
            snack.ThumbImageUrl = imagesManagement.GetImage(snack.Id, ImageType.Thumb);
            snack.StorefrontImageUrl = imagesManagement.GetImage(snack.Id, ImageType.Storefront);
            snack.DetailImageUrl = imagesManagement.GetImage(snack.Id, ImageType.Detail);
        }
    }
}
