using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Snacks.Repositories.Interfaces;
using Snacks.Models;
using System;

namespace Snacks.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IBasketRepository _basketRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISession _session;
        private readonly Guid _basketId;

        public OrderController(IOrderRepository orderRepository, IBasketRepository basketRepository, IHttpContextAccessor httpContextAccessor)
        {
            _orderRepository = orderRepository;
            _basketRepository = basketRepository;
            _httpContextAccessor = httpContextAccessor;
            _session = _httpContextAccessor.HttpContext.Session;

            Guid.TryParse(_session.GetString("BasketId"), out _basketId);
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            IActionResult result;

            if (_basketId == Guid.Empty)
            {
                result = RedirectToAction("Index", "Basket");
            }
            else
            {
                result = View();
            }

            return result;
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            IActionResult result = View(order);

            if (_basketId == Guid.Empty)
            {
                result = RedirectToAction("Index", "Basket");
            }
            else
            {
                var basket = _basketRepository.GetBasket(_basketId);
                if (basket != null)
                {
                    if (ModelState.IsValid)
                    {
                        _orderRepository.CreateOrder(basket, order);
                        _session.SetString("BasketId", Guid.Empty.ToString());

                        result = RedirectToAction("Confirmation", new { orderId = order.Id, basketId = order.BasketId });
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Carrinho de compras inválido.");
                }
            }

            return result;
        }

        [HttpGet]
        public IActionResult Confirmation(int orderId, Guid basketId)
        {
            IActionResult result;

            if (orderId > 0 && basketId != Guid.Empty)
            {
                var order = _orderRepository.GetOrder(orderId, basketId);
                if (order != null)
                {
                    result = View(order);
                }
                else
                {
                    result = RedirectToAction("Index", "Basket");
                }
            }
            else
            {
                result = RedirectToAction("Index", "Basket");
            }

            return result;
        }
    }
}