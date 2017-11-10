using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SignalRChatMVC.Infrastructure.Abstract;
using SignalRChatMVC.Controllers;
using SignalRChatMVC.Domain.Entities;
using System.Web.Mvc;
using SignalRChatMVC.Infrastructure;
using SignalRChatMVC.Infrastructure.Concrete;

namespace SignalRChatMVC.Tests
{
    [TestClass]
    public class AccountControllerTests
    {
        [TestMethod]
        public void User_Can_Login_With_Valid_Credentials()
        {
            // Arrange
            var mock = new Mock<IAuthProvider>();
            mock.Setup(x => x.Login("user", "test")).Returns(true);
            AccountController controller = new AccountController(mock.Object);
            User model = new User()
            {
                UserName = "user",
                Password = "test"
            };

            // Act
            var result = controller.Login(model);

            // Assert
            mock.Verify(m => m.Login(model.UserName, model.Password));
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void User_Cannot_Login_With_Invalid_Credentials()
        {
            // Arrange
            var mock = new Mock<IAuthProvider>();
            AccountController controller = new AccountController(mock.Object);
            User model = new User()
            {
                UserName = "user",
                Password = "test"
            };
            mock.Setup(x => x.Login(model.UserName, model.Password)).Returns(false);

            // Act
            var result = controller.Login(model);

            // Assert
            Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void User_Can_Register()
        {
            // Arrange
            var mock = new Mock<IAuthProvider>();
            var user = new User() { Password = "test", UserName = "user1" };
            mock.Setup(x => x.Register(user)).Returns(true);
            AccountController controller = new AccountController(mock.Object);

            // Act
            var result = controller.Registration(user);

            // Assert
            mock.Verify(x => x.Register(user));
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void User_Cannot_Register_With_Taken_Username()
        {
            // Arrange
            var mock = new Mock<IAuthProvider>();
            var user = new User() { Password = "test", UserName = "user1" };
            mock.Setup(x => x.Register(user)).Returns(false);
            AccountController controller = new AccountController(mock.Object);

            // Act
            var result = controller.Registration(user);

            // Assert
            Assert.IsFalse(controller.ViewData.ModelState.IsValid);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
