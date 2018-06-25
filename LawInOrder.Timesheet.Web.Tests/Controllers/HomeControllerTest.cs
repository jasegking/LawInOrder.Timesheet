using LawInOrder.Timesheet.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace LawInOrder.Timesheet.Web.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Login()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Login() as ViewResult;

            // Assert
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }

        [TestMethod]
        public void AddTime()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.AddTime() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
