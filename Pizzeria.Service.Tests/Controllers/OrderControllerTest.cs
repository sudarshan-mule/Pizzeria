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
    public class OrderControllerTest
    {
        [TestMethod]
        public void SaveOrder_Test()
        {
            // Arrange
            int expected = 1234;
            OrderViewModel order = new OrderViewModel()
            {
                Food = new FoodViewModel() { Image = "../../../assets/img/Regular_Pizza.jpg", Name = "'Regular Pizza", Price = 50 },
                Size = "Medium",
                Quantity = 2,
                Ingredients = new List<IngredientsViewModel>() { new IngredientsViewModel(){ Name = "Flour", IsChecked=false, Price = 40},
                new IngredientsViewModel(){ Name = "Mozzarella", IsChecked=false, Price = 30} },
                Toppings = new List<ToppingsViewModel>() { new ToppingsViewModel(){ Name = "Pepperoni", IsChecked=false, Price = 10},
                new ToppingsViewModel(){ Name = "Sausage", IsChecked=false, Price = 15} },
                GrandTotal = 220
            };
            var repoMock = new Mock<IOrderBL>(MockBehavior.Strict);
            repoMock.Setup(r => r.saveOrder(It.IsAny<OrderViewModel>())).Returns(expected);
            var controller = new OrderController(repoMock.Object);

            // Act
            var orderDetails = controller.SaveOrder(order);
            var contentResult = orderDetails as JsonResult<int>;

            // Assert
            Assert.AreEqual(expected, contentResult.Content);
        }

        [TestMethod]
        public void GetAllIngredients_Test()
        {
            // Arrange
            var expected = new List<IngredientsViewModel>()
            {
                new IngredientsViewModel(){ Name = "Flour", IsChecked=false, Price = 40},
                new IngredientsViewModel(){ Name = "Mozzarella", IsChecked=false, Price = 30}
            };
            var repoMock = new Mock<IOrderBL>(MockBehavior.Strict);
            repoMock.Setup(r => r.GetAllIngredients()).Returns(expected);
            var controller = new OrderController(repoMock.Object);

            // Act
            var ingredients = controller.GetAllIngredients();
            var actual = (ingredients as OkNegotiatedContentResult<List<IngredientsViewModel>>).Content;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetAllToppings_Test()
        {
            // Arrange
            var expected = new List<ToppingsViewModel>()
            {
                new ToppingsViewModel(){ Name = "Pepperoni", IsChecked=false, Price = 10},
                new ToppingsViewModel(){ Name = "Sausage", IsChecked=false, Price = 15}
            };
            var repoMock = new Mock<IOrderBL>(MockBehavior.Strict);
            repoMock.Setup(r => r.GetAllToppings()).Returns(expected);
            var controller = new OrderController(repoMock.Object);

            // Act
            var toppings = controller.GetAllToppings();
            var actual = (toppings as OkNegotiatedContentResult<List<ToppingsViewModel>>).Content;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetSize_Test()
        {
            // Arrange
            var expected = new List<string>()
            {
                "Small", "Medium", "Large"
            };
            var repoMock = new Mock<IOrderBL>(MockBehavior.Strict);
            repoMock.Setup(r => r.GetSize()).Returns(expected);
            var controller = new OrderController(repoMock.Object);

            // Act
            var size = controller.GetSize();
            var actual = (size as OkNegotiatedContentResult<List<string>>).Content;

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
