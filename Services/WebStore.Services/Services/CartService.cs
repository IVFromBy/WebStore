using System.Linq;
using WebStore.Domain;
using WebStore.Domain.Entites;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Mapping;
using WebStore.Interfaces.Services;
using WebStore.ViewModels;

namespace WebStore.Services.Services
{
    public class CartService : ICartService
    {

        private readonly IProductData _productData;
        private readonly ICartStore _cartStore;


        public CartService(ICartStore cartStore, IProductData productData)
        {

            _cartStore = cartStore;
            _productData = productData;

        }
        public void Add(int id)
        {
            var cart = _cartStore.Cart;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
            if (item is null)
                cart.Items.Add(new CartItem { ProductId = id });
            else
                item.Quantity++;

            _cartStore.Cart = cart;
        }

        public void Clear()
        {
            var cart = _cartStore.Cart;
            cart.Items.Clear();

            _cartStore.Cart = cart;
        }

        public void Decrement(int id)
        {
            var cart = _cartStore.Cart;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
            if (item is null) return;

            if (item.Quantity > 0)
                item.Quantity--;

            if (item.Quantity == 0)
                cart.Items.Remove(item);

            _cartStore.Cart = cart;
        }

        public CartViewModel GetViewModel()
        {
            var products = _productData.GetProducts(new ProductFilter
            {
                Ids = _cartStore.Cart.Items.Select(item => item.ProductId).ToArray()
            });

            var product_view_models = products.Products.FromDto().ToView().ToDictionary(p => p.Id);

            return new CartViewModel
            {
                Items = _cartStore.Cart.Items
                .Where(item => product_view_models.ContainsKey(item.ProductId))
                .Select(item => (product_view_models[item.ProductId], item.Quantity))
            };

        }

        public void Remove(int id)
        {
            var cart = _cartStore.Cart;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == id);
            if (item is null) return;

            cart.Items.Remove(item);

            _cartStore.Cart = cart;
        }
    }
}
