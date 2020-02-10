using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Snacks.Repositories.Interfaces;
using Snacks.ViewModels;
using System;

namespace Snacks.Components
{
    public class SummaryBasket : ViewComponent
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;
        private Guid _basketId;

        public SummaryBasket(IBasketRepository basketRepository, IHttpContextAccessor httpContextAccessor)
        {
            _basketRepository = basketRepository;
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;

            Guid.TryParse(_session.GetString("BasketId"), out _basketId);
        }

        public IViewComponentResult Invoke()
        {
            var basketViewModel = new BasketViewModel();

            if (_basketId != Guid.Empty)
            {
                var basket = _basketRepository.GetBasket(_basketId);
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
