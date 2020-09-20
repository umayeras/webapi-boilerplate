using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using WebApp.Business.Abstract.Services;
using WebApp.Controllers;
using WebApp.Core.Resources;
using WebApp.Model.Entities;
using WebApp.Model.Requests;
using WebApp.Model.Results;
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
        public async Task Get_NoCondition_ReturnList()
        {
            // Arrange
            var sampleList = new List<Sample>();
            var apiResult = new OkObjectResult(sampleList);

            sampleService.Setup(x => x.GetAll()).ReturnsAsync(sampleList);

            // Act
            var result = await controller.Get();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            result.Should().BeEquivalentTo(apiResult);
        }

        [Test]
        public async Task Post_InvalidRequest_ReturnBadRequest()
        {
            // Arrange
            var request = new AddSampleRequest();
            var validationResult = ValidationResult.Error("invalid request");
            var apiResult = new BadRequestObjectResult(Messages.InvalidRequest);

            requestValidator.Setup(x => x.Validate(request)).Returns(validationResult);

            // Act
            var result = await controller.Post(request);

            // Assert
            result.Should().BeEquivalentTo(apiResult);
        }

        [Test]
        public async Task Post_AddingFails_ReturnErrorResult()
        {
            // Arrange
            var request = new AddSampleRequest();
            var validationResult = ValidationResult.Success;
            var serviceResult = ServiceResult.Error();
            var apiResult = new OkObjectResult(serviceResult);

            requestValidator.Setup(x => x.Validate(request)).Returns(validationResult);
            sampleService.Setup(x => x.Add(request)).ReturnsAsync(serviceResult);

            // Act
            var result = await controller.Post(request);

            // Assert
            result.Should().BeEquivalentTo(apiResult);
        }

        [Test]
        public async Task Post_AddingSucceeds_ReturnSuccessResult()
        {
            // Arrange
            var request = new AddSampleRequest();
            var validationResult = ValidationResult.Success;
            var serviceResult = ServiceResult.Success();
            var apiResult = new OkObjectResult(serviceResult);

            requestValidator.Setup(x => x.Validate(request)).Returns(validationResult);
            sampleService.Setup(x => x.Add(request)).ReturnsAsync(serviceResult);

            // Act
            var result = await controller.Post(request);

            // Assert
            result.Should().BeEquivalentTo(apiResult);
        }

        [Test]
        public async Task Put_InvalidRequest_ReturnBadRequest()
        {
            // Arrange
            var request = new UpdateSampleRequest();
            var validationResult = ValidationResult.Error("invalid request");
            var apiResult = new BadRequestObjectResult(Messages.InvalidRequest);

            requestValidator.Setup(x => x.Validate(request)).Returns(validationResult);

            // Act
            var result = await controller.Put(request);

            // Assert
            result.Should().BeEquivalentTo(apiResult);
        }

        [Test]
        public async Task Put_AddingFails_ReturnErrorResult()
        {
            // Arrange
            var request = new UpdateSampleRequest();
            var validationResult = ValidationResult.Success;
            var serviceResult = ServiceResult.Error();
            var apiResult = new OkObjectResult(serviceResult);

            requestValidator.Setup(x => x.Validate(request)).Returns(validationResult);
            sampleService.Setup(x => x.Update(request)).ReturnsAsync(serviceResult);

            // Act
            var result = await controller.Put(request);

            // Assert
            result.Should().BeEquivalentTo(apiResult);
        }

        [Test]
        public async Task Put_AddingSucceeds_ReturnSuccessResult()
        {
            // Arrange
            var request = new UpdateSampleRequest();
            var validationResult = ValidationResult.Success;
            var serviceResult = ServiceResult.Success();
            var apiResult = new OkObjectResult(serviceResult);

            requestValidator.Setup(x => x.Validate(request)).Returns(validationResult);
            sampleService.Setup(x => x.Update(request)).ReturnsAsync(serviceResult);

            // Act
            var result = await controller.Put(request);

            // Assert
            result.Should().BeEquivalentTo(apiResult);
        }
        
        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public async Task Delete_InvalidRequest_ReturnBadRequest(int id)
        {
            // Arrange
            var apiResult = new BadRequestObjectResult(Messages.InvalidRequest);

            // Act
            var result = await controller.Delete(id);

            // Assert
            result.Should().BeEquivalentTo(apiResult);
        }
        
        [Test]
        public async Task Delete_DeletingFails_ReturnErrorResult()
        {
            // Arrange
            const int id = 1;
            var serviceResult = ServiceResult.Error();
            var apiResult = new OkObjectResult(serviceResult);

            sampleService.Setup(x => x.Delete(id)).ReturnsAsync(serviceResult);
            
            // Act
            var result = await controller.Delete(id);

            // Assert
            result.Should().BeEquivalentTo(apiResult);
        }
        
        [Test]
        public async Task Delete_DeletingSucceeds_ReturnSuccessResult()
        {
            // Arrange
            const int id = 1;
            var serviceResult = ServiceResult.Success();
            var apiResult = new OkObjectResult(serviceResult);

            sampleService.Setup(x => x.Delete(id)).ReturnsAsync(serviceResult);
            
            // Act
            var result = await controller.Delete(id);

            // Assert
            result.Should().BeEquivalentTo(apiResult);
        }
    }
}