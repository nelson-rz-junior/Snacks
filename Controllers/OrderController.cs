using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Snacks.Repositories.Interfaces;
using Snacks.Models;
using System;
using Microsoft.AspNetCore.Identity;
using Snacks.Context;
using System.Threading.Tasks;
using Snacks.Extensions;
using Snacks.Services.Images;
using System.Linq;

namespace Snacks.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IBasketRepository _basketRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IImagesManagement _imagesManagement;
        private readonly ISession _session;
        private readonly Guid _basketId;

        public OrderController(IOrderRepository orderRepository, IBasketRepository basketRepository, IHttpContextAccessor httpContextAccessor, 
            UserManager<ApplicationUser> userManager, IImagesManagement imagesManagement)
        {
            _orderRepository = orderRepository;
            _basketRepository = basketRepository;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _imagesManagement = imagesManagement;
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

                        result = RedirectToAction("Confirmation", new { orderId = order.Id });
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
        public async Task<IActionResult> Confirmation(int orderId)
        {
            IActionResult result;

            if (orderId > 0)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    var order = _orderRepository.GetOrder(orderId, user);
                    if (order != null)
                    {
                        order.OrderItems.Select(o => o.Snack).SetImages(_imagesManagement);
                        result = View(order);
                    }
                    else
                    {
                        result = RedirectToAction("Index", "Basket");
                    }
                }
                else
                {
                    result = RedirectToAction("Login", "Account");
                }
            }
            else
            {
                result = RedirectToAction("Index", "Basket");
            }

            return result;
        }

        [HttpGet]
        public async Task<IActionResult> MyOrders()
        {
            IActionResult result;

            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var snacks = _orderRepository.GetOrders(user);
                result = View(snacks);
            }
            else
            {
                result = RedirectToAction("Login", "Account");
            }

            return result;
        }

        [HttpGet]
        public async Task<IActionResult> OrderDetails(int orderId)
        {
            IActionResult result;

            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var order = _orderRepository.GetOrder(orderId, user);
                if (order != null)
                {
                    order.OrderItems.Select(o => o.Snack).SetImages(_imagesManagement);
                    result = View(order);
                }
                else
                {
                    result = RedirectToAction("Index", "Basket");
                }
            }
            else
            {
                result = RedirectToAction("Login", "Account");
            }

            return result;
        }
    }
}