using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService) => _cartService = cartService;

        public IActionResult Index => View(_cartService.GetViewModel());

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
    }
}
