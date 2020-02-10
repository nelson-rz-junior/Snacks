using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Snacks.Models;
using System;

namespace Snacks.Services.Images
{
    public class ImagesManagement : IImagesManagement
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IMemoryCache _memoryCache;

        public ImagesManagement(IWebHostEnvironment hostingEnvironment, IMemoryCache memoryCache)
        {
            _hostingEnvironment = hostingEnvironment;
            _memoryCache = memoryCache;
        }

        public string GetImage(int snackId, ImageType imageType)
        {
            string keyCache = $"{CacheKeys.SnackImage}-{snackId}-{imageType.ToString()}";
            if (!_memoryCache.TryGetValue(keyCache, out string relativePath))
            {
                string imageFile = $"{imageType.ToString().ToLower()}.png";
                string path = $"{_hostingEnvironment.WebRootPath}\\images\\products\\{snackId}\\{imageFile}";
                if (System.IO.File.Exists(path))
                {
                    relativePath = $"/images/products/{snackId}/{imageFile}";
                }
                else
                {
                    relativePath = $"/images/products/unavailable_{imageFile}";
                }

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(300));

                _memoryCache.Set(keyCache, relativePath, cacheEntryOptions);
            }

            return relativePath;
        }
    }
}
