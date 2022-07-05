using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BidOneTest.WebApi.Controllers;
using BidOneTest.WebApi.Interfaces;
using BidOneTest.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BidOneTest.Tests.Controllers
{
    [TestClass]
    public class DefaultControllerTests
    {
        private readonly IFileHandler _fileHandler;
        private readonly DefaultController _defaultController;

        public DefaultControllerTests()
        {
            this._fileHandler = new Mock<IFileHandler>().Object;
            this._defaultController = new DefaultController(this._fileHandler);
        }

        [TestMethod]
        public async Task IndexPost_ReturnsBadRequestResult_WhenFirstNameNotProvided()
        {
            // Arrange
            this._defaultController.ModelState.AddModelError("FirstName", "FirstName is required");

            // Act
            var actionResult = await this._defaultController.Index(new Model(firstName: It.IsAny<string>(), lastName: "LastName"));

            // Assert
            Assert.IsTrue(this._defaultController.ModelState.ValidationState == ModelValidationState.Invalid);
            Assert.IsTrue(this._defaultController.ModelState.Values.SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).Contains("FirstName is required"));

            var contentResult = actionResult as BadRequestObjectResult;

            Assert.IsNotNull(contentResult);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, contentResult.StatusCode);
        }

        [TestMethod]
        public async Task IndexPost_ReturnsBadRequestResult_WhenLastNameNotProvided()
        {
            // Arrange
            this._defaultController.ModelState.AddModelError("LastName", "LastName is required");

            // Act
            var actionResult = await this._defaultController.Index(new Model(firstName: "FirstName", lastName: It.IsAny<string>()));

            // Assert
            Assert.IsTrue(this._defaultController.ModelState.ValidationState == ModelValidationState.Invalid);
            Assert.IsTrue(this._defaultController.ModelState.Values.SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).Contains("LastName is required"));

            var contentResult = actionResult as BadRequestObjectResult;

            Assert.IsNotNull(contentResult);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, contentResult.StatusCode);
        }

        [TestMethod]
        public async Task IndexPost_ReturnsBadRequestResult_WhenFirstNameAndLastNameNotProvided()
        {
            // Arrange
            this._defaultController.ModelState.AddModelError("FirstName", "FirstName is required");
            this._defaultController.ModelState.AddModelError("LastName", "LastName is required");

            // Act
            var actionResult =
                await this._defaultController.Index(new Model(firstName: It.IsAny<string>(),
                    lastName: It.IsAny<string>()));

            // Assert
            Assert.IsTrue(this._defaultController.ModelState.ValidationState == ModelValidationState.Invalid);
            Assert.IsTrue(this._defaultController.ModelState.Values.SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).Contains("FirstName is required"));
            Assert.IsTrue(this._defaultController.ModelState.Values.SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).Contains("LastName is required"));

            var contentResult = actionResult as BadRequestObjectResult;
            Assert.IsNotNull(contentResult);
            Assert.AreEqual((int)HttpStatusCode.BadRequest, contentResult.StatusCode);
        }

        [TestMethod]
        public async Task IndexPost_ReturnsOkResult_WhenFirstNameAndLastNameProvided()
        {
            // Act
            var actionResult = await this._defaultController.Index(new Model(firstName: "Test", "Passed"));

            // Assert
            Assert.IsTrue(this._defaultController.ModelState.ValidationState == ModelValidationState.Valid);

            var contentResult = actionResult as OkResult;
            Assert.IsNotNull(contentResult);
            Assert.AreEqual((int)HttpStatusCode.OK, contentResult.StatusCode);
        }
    }
}