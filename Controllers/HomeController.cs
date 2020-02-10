using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Snacks.Repositories.Interfaces;
using Snacks.Models;
using Snacks.ViewModels;
using System.Diagnostics;
using System.Collections.Generic;
using Snacks.Services.Images;
using Snacks.Extensions;

namespace Snacks.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISnackRepository _snackRepository;
        private readonly IImagesManagement _imagesManagement;

        public HomeController(ILogger<HomeController> logger, ISnackRepository snackRepository, IImagesManagement imagesManagement)
        {
            _logger = logger;
            _snackRepository = snackRepository;
            _imagesManagement = imagesManagement;
        }

        public IActionResult Index()
        {
            IEnumerable<Snack> favoriteSnacks = _snackRepository.FavoriteSnacks;

            var homeViewModel = new HomeViewModel
            {
                FavoriteSnacks = favoriteSnacks
            };

            favoriteSnacks.SetImages(_imagesManagement);

            return View(homeViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
