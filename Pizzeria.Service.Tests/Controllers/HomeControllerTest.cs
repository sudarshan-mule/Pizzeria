using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pizzeria.Repositories;
using Pizzeria.Service.Controllers;
using Pizzeria.ViewModel;
using System.Collections.Generic;
using System.Web.Http.Results;

namespace Pizzeria.Service.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void GetAllFood_Test()
        {
            // Arrange
            var expected = new List<FoodViewModel>()
            {
                new FoodViewModel(){ Image = "../../../assets/img/Regular_Pizza.jpg", Name = "'Regular Pizza", Price = 50},
                new FoodViewModel(){ Image = "../../../assets/img/Cheese_Pizza.jpg", Name = "'Cheese Pizza", Price = 75}
            };
            var repoMock = new Mock<IHomeBL>(MockBehavior.Strict);
            repoMock.Setup(r => r.GetAllFood()).Returns(expected);
            var controller = new HomeController(repoMock.Object);

            // Act
            var orderDetails = controller.GetAllFood();
            var actual = (orderDetails as OkNegotiatedContentResult<List<FoodViewModel>>).Content;

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
