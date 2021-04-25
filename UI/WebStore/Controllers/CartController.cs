using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entites.DTO;
using WebStore.Infrastructure.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService) => _cartService = cartService;

        public IActionResult Index() => View(new CartOrderViewModel { Cart = _cartService.GetViewModel() });

        public IActionResult Add(int id)
        {
            _cartService.Add(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int id)
        {
            _cartService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Decrement(int id)
        {
            _cartService.Decrement(id);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Clear()
        {
            _cartService.Clear();
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> CheckOut(OrderViewModel orderModel, [FromServices] IOrderService orderService)
        {
            if (!ModelState.IsValid)
                return View(nameof(Index), new CartOrderViewModel
                {
                    Cart = _cartService.GetViewModel(),
                    Order = orderModel,
                });

            //var order = await orderService.CreateOrder(
            //    User.Identity.Name,
            //    _cartService.GetViewModel(),
            //    orderModel
            //    );
            var order_model = new CreateOrderModel
            {
                Order = orderModel,
                Items = _cartService.GetViewModel().Items.Select(item => new OrderItemDto
                {
                    Id = item.Product.Id,
                    Price = item.Product.Price,
                    Quantity = item.Quantity,

                }).ToList()
            };
            var order = await orderService.CreateOrder(User.Identity.Name, order_model);

            _cartService.Clear();

            return RedirectToAction(nameof(OrderConfirmed), new { order.Id});
        }

        public IActionResult OrderConfirmed(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }

    }
}
