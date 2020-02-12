using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Snacks.Context;
using Snacks.Repositories.Interfaces;
using Snacks.ViewModels;
using System;
using System.Security.Claims;

namespace Snacks.Components
{
    public class SummaryBasket : ViewComponent
    {
        private readonly IBasketRepository _basketRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;
        private Guid _basketId;

        public SummaryBasket(IBasketRepository basketRepository, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _basketRepository = basketRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;

            Guid.TryParse(_session.GetString("BasketId"), out _basketId);
        }

        public IViewComponentResult Invoke(ClaimsPrincipal claimsPrincipal)
        {
            var basketViewModel = new BasketViewModel();

            if (_basketId != Guid.Empty)
            {
                var userId = _userManager.GetUserId(claimsPrincipal);
                var basket = _basketRepository.GetBasket(_basketId, userId);
                if (basket != null)
                {
                    basketViewModel.BasketItems = basket.BasketItems;
                    basketViewModel.Total = basket.Total;
                }
            }

            return View(basketViewModel);
        }
    }
}
