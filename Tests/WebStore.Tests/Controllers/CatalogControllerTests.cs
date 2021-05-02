using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebStore.Controllers;
using WebStore.Domain.Entites.DTO;
using WebStore.Infrastructure.Interfaces;
using WebStore.ViewModels;
using Assert = Xunit.Assert;

namespace WebStore.Tests.Controllers
{
    [TestClass]
    public class CatalogControllerTests
    {
        [TestMethod]
        public void Details_Return_with_Correct_View()
        {
            #region Arrange

            // Подготовка исходных данных
            // Подготовка ожидаемых результатов
            // Подготовка объекта тестирования

            const int expected_id = 1;
            const decimal expected_price = 10m;

            var expected_name = $"product id {expected_id}";

            var product_data_mock = new Mock<IProductData>();
            product_data_mock.Setup(p => p.GetProductById(It.IsAny<int>()))
                .Returns<int>(id => new ProductDto
                {
                    Id = id,
                    Name = $"product id {id}",
                    Order = 1,
                    Price = expected_price,
                    Image = $"img_{id}.png",
                    Brand = new BrandDto { Id = 1, Name = "Brand", Order = 1 },
                    Section = new SectionDto { Id = 1, Order = 1, Name = "Section" },


                })

                ;


            var controller = new CatalogController(product_data_mock.Object);
            #endregion

            #region Act
            // Выполнение действия 
            var result = controller.Details(expected_id);

            #endregion

            #region Assert

            // Проверка утверждений

            var view_result = Assert.IsType<ViewResult>(result);
            var mode = Assert.IsAssignableFrom<ProductViewModel>(view_result.Model);

            Assert.Equal(expected_id, mode.Id);
            Assert.Equal(expected_name, mode.Name);
            Assert.Equal(expected_price, mode.Price);
            #endregion
        }


    }
}
