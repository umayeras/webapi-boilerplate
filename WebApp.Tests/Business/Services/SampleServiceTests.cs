using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using WebApp.Business.Abstract.Factories;
using WebApp.Business.Services;
using WebApp.Core.Resources;
using WebApp.Data.Repositories;
using WebApp.Data.Uow;
using WebApp.Model.Entities;
using WebApp.Model.Requests;
using WebApp.Model.Results;
using WebApp.Tests.Helpers;
using WebApp.Core.Extensions;

namespace WebApp.Tests.Business.Services
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class SampleServiceTests
    {
        #region members & setup

        private SampleService service;

        private Mock<ILogger<SampleService>> logger;
        private Mock<IUnitOfWork> unitOfWork;
        private Mock<IBaseRepository<Sample>> repository;
        private Mock<ISampleFactory> sampleFactory;

        [SetUp]
        public void Init()
        {
            logger = new Mock<ILogger<SampleService>>();
            unitOfWork = new Mock<IUnitOfWork>();
            repository = new Mock<IBaseRepository<Sample>>();
            sampleFactory = new Mock<ISampleFactory>();
            unitOfWork.Setup(x => x.GetRepository<Sample>()).Returns(repository.Object);
            
            service = new SampleService(unitOfWork.Object, logger.Object, sampleFactory.Object);
        }

        #endregion

        [Test]
        public async Task GetAll_NoCondition_ReturnList()
        {
            // Arrange
            var list = new List<Sample> {new Sample {Id = 1}}.AsQueryable();
            
            repository.Setup(x => x.GetAllAsync(null)).ReturnsAsync(list);

            // Act
            var result = await service.GetAll();

            // Assert
            result.Should().BeEquivalentTo(list);
        }
        
        [Test]
        public async Task Add_AddingFailed_AddLogAndReturnServiceErrorResult()
        {
            // Arrange
            var request = new AddSampleRequest();
            var model = new Sample();
            var exception = It.IsAny<Exception>();
            var serviceResult = ServiceResult.Error(Messages.AddingFailed);

            sampleFactory.Setup(x => x.CreateAddSample(request)).Returns(model);
            repository.Setup(x => x.AddAsync(model)).ThrowsAsync(exception);

            // Act
            var result = await service.Add(request);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Be(serviceResult.Message);
            unitOfWork.Verify(x=>x.Save(), Times.Never);
            AssertHelpers.VerifyLogger(logger, LogLevel.Error, Times.Once());
        }
        
        [Test]
        public async Task Add_AddingSuccess_AddToDbAndReturnServiceSuccessResult()
        {
            // Arrange
            var request = new AddSampleRequest();
            var model = new Sample();
            const bool isAdded = true;
            var serviceResult = ServiceResult.Success(Messages.AddingSuccess);
            
            sampleFactory.Setup(x => x.CreateAddSample(request)).Returns(model);
            repository.Setup(x => x.AddAsync(model)).ReturnsAsync(isAdded);

            // Act
            var result = await service.Add(request);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Message.Should().Be(serviceResult.Message);
            unitOfWork.Verify(x=>x.Save(), Times.Once);
        }
        
        [Test]
        public async Task Update_UpdatingFailed_AddLogAndReturnServiceErrorResult()
        {
            // Arrange
            var request = new UpdateSampleRequest{Id = 1};
            var model = new Sample{Id = 1, CreatedDate = It.IsAny<DateTime>()};
            var exception = It.IsAny<Exception>();
            var serviceResult = ServiceResult.Error(Messages.UpdatingFailed);

            repository.Setup(x => x.GetAsync(s => s.Id == request.Id)).ReturnsAsync(model);
            sampleFactory.Setup(x => x.CreateUpdateSample(model, request)).Returns(model);
            repository.Setup(x => x.Update(model)).Throws(exception);

            // Act
            var result = await service.Update(request);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Be(serviceResult.Message);
            unitOfWork.Verify(x=>x.Save(), Times.Never);
            AssertHelpers.VerifyLogger(logger, LogLevel.Error, Times.Once());
        }
        
        [Test]
        public async Task Update_UpdatingSuccess_UpdateEntityAndReturnServiceSuccessResult()
        {
            // Arrange
            var request = new UpdateSampleRequest{Id = 1};
            var model = new Sample{Id = 1, CreatedDate = It.IsAny<DateTime>()};
            const bool isUpdated = true;
            var serviceResult = ServiceResult.Error(Messages.UpdatingSuccess);

            repository.Setup(x => x.GetAsync(s => s.Id == request.Id)).ReturnsAsync(model);
            sampleFactory.Setup(x => x.CreateUpdateSample(model, request)).Returns(model);
            repository.Setup(x => x.Update(model)).Returns(isUpdated);

            // Act
            var result = await service.Update(request);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Message.Should().Be(serviceResult.Message);
            unitOfWork.Verify(x=>x.Save(), Times.Once);
        }
        
        [Test]
        public async Task Delete_DeletingFailed_AddLogAndReturnServiceErrorResult()
        {
            // Arrange
            const int id = 1;
            var model = new Sample{Id = id};
            var exception = It.IsAny<Exception>();
            var serviceResult = ServiceResult.Error(Messages.DeletingFailed);

            repository.Setup(x => x.GetAsync(s => s.Id == id)).ReturnsAsync(model);
            repository.Setup(x => x.Delete(model)).Throws(exception);

            // Act
            var result = await service.Delete(id);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Message.Should().Be(serviceResult.Message);
            unitOfWork.Verify(x=>x.Save(), Times.Never);
            AssertHelpers.VerifyLogger(logger, LogLevel.Error, Times.Once());
        }
        
        [Test]
        public async Task Delete_DeletingSuccess_UpdateEntityStatusForDeletedAndReturnServiceSuccessResult()
        {
            // Arrange
            var id = 1;
            var model = new Sample {Id = 1, StatusId = StatusType.Deleted.ToInt32()};
            const bool isDeleted = true;
            var serviceResult = ServiceResult.Success(Messages.DeletingSuccess);
            
            repository.Setup(x => x.GetAsync(s => s.Id == id)).ReturnsAsync(model);
            repository.Setup(x => x.Delete(model)).Returns(isDeleted);

            // Act
            var result = await service.Delete(id);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Message.Should().Be(serviceResult.Message);
            unitOfWork.Verify(x=>x.Save(), Times.Once);
        }
    }
}