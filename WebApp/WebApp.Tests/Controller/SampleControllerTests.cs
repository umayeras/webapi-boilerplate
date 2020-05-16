using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using WebApp.Business.Abstract;
using WebApp.Controllers;
using WebApp.Model;
using WebApp.Validation;
using WebApp.Validation.Abstract;

namespace WebApp.Tests.Controller
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class SampleControllerTests
    {
        #region members & setup

        private SampleController controller;
        private Mock<IRequestValidator> requestValidator;
        private Mock<ISampleService> sampleService;

        [SetUp]
        public void Init()
        {
            requestValidator = new Mock<IRequestValidator>();
            sampleService = new Mock<ISampleService>();

            controller = new SampleController(requestValidator.Object, sampleService.Object);
        }

        #endregion

        [Test]
        public void Get_NoCondition_ReturnList()
        {
            // Arrange
            var sampleList = new List<Sample>();
            var apiResult = new OkObjectResult(sampleList);

            sampleService.Setup(x => x.Get()).ReturnsAsync(sampleList);

            // Act
            var result = controller.Get();

            // Assert
            result.Should().BeOfType<Task<IActionResult>>();
            result.Result.Should().BeEquivalentTo(apiResult);
        }

        [Test]
        public void Post_InvalidRequest_ReturnBadRequest()
        {
            // Arrange
            var request = new AddSampleRequest();
            var validationResult = ValidationResult.Error("invalid request");
            var apiResult = new BadRequestObjectResult("InvalidRequest");
            
            requestValidator.Setup(x => x.Validate(request)).Returns(validationResult);
            
            // Act
            var result = controller.Post(request);

            // Assert
            result.Should().BeEquivalentTo(apiResult);
        }
        
        [Test]
        public void Post_AddingFails_ReturnErrorResult()
        {
            // Arrange
            var request = new AddSampleRequest();
            var validationResult = ValidationResult.Success;
            var serviceResult = ServiceResult.Error();
            var apiResult = new OkObjectResult(serviceResult);
            
            requestValidator.Setup(x => x.Validate(request)).Returns(validationResult);
            sampleService.Setup(x => x.Add(request)).Returns(serviceResult);
            
            // Act
            var result = controller.Post(request);

            // Assert
            result.Should().BeEquivalentTo(apiResult);
        }
        
        [Test]
        public void Post_AddingSucceeds_ReturnSuccessResult()
        {
            // Arrange
            var request = new AddSampleRequest();
            var validationResult = ValidationResult.Success;
            var serviceResult = ServiceResult.Success();
            var apiResult = new OkObjectResult(serviceResult);
            
            requestValidator.Setup(x => x.Validate(request)).Returns(validationResult);
            sampleService.Setup(x => x.Add(request)).Returns(serviceResult);
            
            // Act
            var result = controller.Post(request);

            // Assert
            result.Should().BeEquivalentTo(apiResult);
        }
        
        [Test]
        public void Put_InvalidRequest_ReturnBadRequest()
        {
            // Arrange
            var request = new UpdateSampleRequest();
            var validationResult = ValidationResult.Error("invalid request");
            var apiResult = new BadRequestObjectResult("InvalidRequest");
            
            requestValidator.Setup(x => x.Validate(request)).Returns(validationResult);
            
            // Act
            var result = controller.Put(request);

            // Assert
            result.Should().BeEquivalentTo(apiResult);
        }
        
        [Test]
        public void Put_AddingFails_ReturnErrorResult()
        {
            // Arrange
            var request = new UpdateSampleRequest();
            var validationResult = ValidationResult.Success;
            var serviceResult = ServiceResult.Error();
            var apiResult = new OkObjectResult(serviceResult);
            
            requestValidator.Setup(x => x.Validate(request)).Returns(validationResult);
            sampleService.Setup(x => x.Update(request)).Returns(serviceResult);
            
            // Act
            var result = controller.Put(request);

            // Assert
            result.Should().BeEquivalentTo(apiResult);
        }
        
        [Test]
        public void Put_AddingSucceeds_ReturnSuccessResult()
        {
            // Arrange
            var request = new UpdateSampleRequest();
            var validationResult = ValidationResult.Success;
            var serviceResult = ServiceResult.Success();
            var apiResult = new OkObjectResult(serviceResult);
            
            requestValidator.Setup(x => x.Validate(request)).Returns(validationResult);
            sampleService.Setup(x => x.Update(request)).Returns(serviceResult);
            
            // Act
            var result = controller.Put(request);

            // Assert
            result.Should().BeEquivalentTo(apiResult);
        }
    }
}