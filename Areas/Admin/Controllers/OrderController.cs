using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Snacks.Context;
using System;
using System.Threading.Tasks;

namespace Snacks.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Manager")]
    public class OrderController : Controller
    {
        private readonly SnackContext _context;

        public OrderController(SnackContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var snacks = await _context.Orders
                .ToListAsync();

            return View(snacks);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (order == null)
            {
                return View("NotFound");
            }

            return View(order);
        }

        [HttpGet]
        public async Task<IActionResult> Payment(int id, bool checkPayment)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);

            if (order == null)
            {
                return View("NotFound");
            }

            if (checkPayment)
            {
                order.IsPaid = true;
                order.PaymentDate = DateTime.Now;
            }
            else
            {
                order.IsPaid = false;
                order.PaymentDate = null;
            }

            order.UpdateDate = DateTime.Now;
            _context.Orders.Update(order);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Order");
        }

        [HttpGet]
        public async Task<IActionResult> Delivery(int id, bool checkDelivery)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);

            if (order == null)
            {
                return View("NotFound");
            }

            if (checkDelivery)
            {
                order.IsDelivery = true;
                order.DeliveryDate = DateTime.Now;
            }
            else
            {
                order.IsDelivery = false;
                order.DeliveryDate = null;
            }

            order.UpdateDate = DateTime.Now;
            _context.Orders.Update(order);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Order");
        }
    }
}
