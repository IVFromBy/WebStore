using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Components
{
    public class CartViewComponent : ViewComponent
    {
        private readonly ICartService _cartService;

        public CartViewComponent(ICartService cartService) => _cartService = cartService;
        

        public IViewComponentResult Invoke() => View(_cartService.GetViewModel());
    }
}
