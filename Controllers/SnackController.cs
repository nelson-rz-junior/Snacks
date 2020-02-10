using Microsoft.AspNetCore.Mvc;
using Snacks.Extensions;
using Snacks.Models;
using Snacks.Repositories.Interfaces;
using Snacks.Services.Images;
using Snacks.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Snacks.Controllers
{
    public class SnackController : Controller
    {
        private readonly ISnackRepository _snackRepository;
        private readonly IImagesManagement _imagesManagement;

        public SnackController(ISnackRepository snackRepository, IImagesManagement imagesManagement)
        {
            _snackRepository = snackRepository;
            _imagesManagement = imagesManagement;
        }

        [HttpGet]
        public IActionResult List(string categoryName)
        {
            IEnumerable<Snack> snacks;

            if (string.IsNullOrWhiteSpace(categoryName))
            {
                categoryName = "Todos os produtos";
                snacks = _snackRepository.GetSnacks();
            }
            else 
            {
                snacks = _snackRepository.GetSnacks(categoryName: categoryName);
            }

            snacks.SetImages(_imagesManagement);

            var snackListViewModel = new SnackListViewModel
            {
                Snacks = snacks,
                CurrentCategory = categoryName
            };

            return View(snackListViewModel);
        }

        [HttpGet]
        public IActionResult Details(int snackId)
        {
            IActionResult result;

            var snack = _snackRepository.Snacks
                .FirstOrDefault(s => s.Id == snackId);

            if (snack == null)
            {
                result = Redirect("~/Home/Error");
            }
            else
            {
                snack.SetImages(_imagesManagement);
                result = View(snack);
            }

            return result;
        }

        [HttpGet]
        public IActionResult Search(string q)
        {
            IEnumerable<Snack> snacks;

            if (string.IsNullOrWhiteSpace(q))
            {
                snacks = _snackRepository.GetSnacks();
            }
            else
            {
                snacks = _snackRepository.GetSnacks(searchTerm: q);
            }

            snacks.SetImages(_imagesManagement);

            return View("~/Views/Snack/List.cshtml", new SnackListViewModel
            {
                Snacks = snacks,
                CurrentCategory = "Todos os produtos"
            });
        }
    }
}