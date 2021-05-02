using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebStore.Domain;
using WebStore.Domain.Entites;
using WebStore.Domain.Entites.DTO;
using WebStore.Infrastructure.Interfaces;
using WebStore.ViewModels;
using Assert = Xunit.Assert;

namespace WebStore.Services.Tests.Services
{
    [TestClass]
    public class CartServiceTests
    {
        private Cart _Cart;
        private Mock<IProductData> _ProductDataMock;

        [TestInitialize]
        public void Initialize()
        {
            _Cart = new Cart
            {
                Items = new List<CartItem>
                {
                   new() {ProductId =1, Quantity=1},
                   new() {ProductId =2, Quantity=3},
                }
            };

            _ProductDataMock = new Mock<IProductData>();
            _ProductDataMock.Setup(c => c.GetProducts(It.IsAny<ProductFilter>()))
                .Returns(
                new[]
                {
                    new ProductDto
                    {
                        Id = 1,
                        Name = "Product 1",
                        Order = 1,
                        Image = "Image_1.png",
                        Brand = new BrandDto{Id =1, Name="Brand 1", Order = 1},
                        Section = new SectionDto{Id = 1, Name = "Section 1", Order = 1},
                        Price = 1.1m,
                    },
                    new ProductDto
                    {
                        Id = 2,
                        Name = "Product 2",
                        Order = 2,
                        Image = "Image_2.png",
                        Brand = new BrandDto{Id =2, Name="Brand 2", Order = 2},
                        Section = new SectionDto{Id = 2, Name = "Section 2", Order = 2},
                        Price = 2.2m,
                    },
                    new ProductDto
                    {
                        Id = 3,
                        Name = "Product 3",
                        Order = 3,
                        Image = "Image_3.png",
                        Brand = new BrandDto{Id =3, Name="Brand 3", Order = 3},
                        Section = new SectionDto{Id = 3, Name = "Section 3", Order = 3},
                        Price = 3.3m,
                    },
                }
                );

        }

        [TestMethod]
        public void Cart_Class_ItemsCount_return_Correct_Quantity()
        {
            var cart = _Cart;

            var expected_item_count = _Cart.Items.Sum(i => i.Quantity);

            var actual_item_count = cart.ItemsCount;

            Assert.Equal(expected_item_count, actual_item_count);

        }

        [TestMethod]
        public void CartViewModel_Returns_Correct_ItemsCount()
        {
            var cart_view_mode = new CartViewModel
            {
                Items = new[]
                {
                    (new ProductViewModel { Id = 1, Name = "Product 1", Price = 0.5m},1),
                    (new ProductViewModel { Id = 2, Name = "Product 2", Price = 1.5m }, 3),
                }
            };

            const int expected_count = 4;
            var actual_count = cart_view_mode.ItemsCount;
            Assert.Equal(expected_count, actual_count);
        }

        [TestMethod]
        public void CartViewModel_Returns_Correct_TotalPrice()
        {
            var cart_view_mode = new CartViewModel
            {
                Items = new[]
                {
                    (new ProductViewModel { Id = 1, Name = "Product 1", Price = 0.5m},1),
                    (new ProductViewModel { Id = 2, Name = "Product 2", Price = 1.5m }, 3),
                }
            };

            var expected_Price = cart_view_mode.Items.Sum(i => i.Quantity * i.Product.Price);

            var actual_count = cart_view_mode.TotalPrice;

            Assert.Equal(expected_Price, actual_count);
        }
    }
}
