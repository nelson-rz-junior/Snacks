using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Snacks.Context;
using Snacks.Extensions;
using Snacks.Repositories.Interfaces;
using Snacks.Services.Images;
using Snacks.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Snacks.Controllers
{
    [Authorize]
    public class BasketController : Controller
    {
        private readonly ISnackRepository _snackRepository;
        private readonly IBasketRepository _basketRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;
        private readonly IImagesManagement _imagesManagement;
        private readonly UserManager<ApplicationUser> _userManager;
        private Guid _basketId;

        public BasketController(ISnackRepository snackRepository, IBasketRepository basketRepository, IHttpContextAccessor httpContextAccessor, 
            IImagesManagement imagesManagement, UserManager<ApplicationUser> userManager)
        {
            _snackRepository = snackRepository;
            _basketRepository = basketRepository;
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;
            _imagesManagement = imagesManagement;
            _userManager = userManager;

            Guid.TryParse(_session.GetString("BasketId"), out _basketId);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var basketViewModel = new BasketViewModel();
            
            if (_basketId != Guid.Empty)
            {
                var userId = _userManager.GetUserId(User);
                var basket = _basketRepository.GetBasket(_basketId, userId);
                if (basket != null)
                {
                    basket.BasketItems.Select(s => s.Snack).SetImages(_imagesManagement);
                    basketViewModel.BasketItems = basket.BasketItems;
                    basketViewModel.Total = basket.Total;
                }
            }

            return View(basketViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> AddBasketItem(int snackId)
        {
            IActionResult result;

            var currentSnack = _snackRepository.Snacks
                .FirstOrDefault(s => s.Id == snackId && s.InStock);

            if (currentSnack == null)
            {
                result = View("NotFound");
            }
            else
            {
                if (_basketId == Guid.Empty)
                {
                    var user = await _userManager.GetUserAsync(User);

                    _basketId = _basketRepository.CreateBasket(user);
                    _session.SetString("BasketId", _basketId.ToString());
                }

                _basketRepository.AddItemBasket(currentSnack, 1, _basketId);

                result = RedirectToAction("Index");
            }

            return result;
        }

        [HttpGet]
        public IActionResult RemoveBasketItem(int snackId)
        {
            IActionResult result;

            var currentSnack = _snackRepository.Snacks
                .FirstOrDefault(s => s.Id == snackId);

            if (currentSnack == null || _basketId == Guid.Empty)
            {
                result = View("NotFound");
            }
            else
            {
                _basketRepository.RemoveBasketItem(currentSnack, _basketId);
                result = RedirectToAction("Index");
            }

            return result;
        }
    }
}