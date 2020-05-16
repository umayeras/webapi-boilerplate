using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using WebApp.Business.Abstract;
using WebApp.Business.Services;
using WebApp.Data.Abstract;
using WebApp.Model;

namespace WebApp.Tests.Business.Services
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class SampleServiceTests
    {
        #region members & setup

        private SampleService service;

        private Mock<ISampleFactory> sampleFactory;
        private Mock<ISampleRepository> sampleRepository;

        [SetUp]
        public void Init()
        {
            sampleFactory = new Mock<ISampleFactory>();
            sampleRepository = new Mock<ISampleRepository>();

            service = new SampleService(
                sampleRepository.Object,
                sampleFactory.Object);
        }

        #endregion

        [Test]
        public void GetSampleList_NoCondition_ReturnList()
        {
            // Arrange
            var sampleList = new List<Sample>();

            sampleRepository.Setup(x => x.GetListAsync(null)).ReturnsAsync(sampleList);

            // Act
            var result = service.Get();

            // Assert
            result.Result.Should().BeEquivalentTo(sampleList);
        }

        [Test]
        public void AddSample_AddingFails_ReturnErrorResult()
        {
            // Arrange
            var request = new AddSampleRequest();
            var sample = new Sample();
            const bool addResult = false;

            sampleFactory.Setup(x => x.CreateAddSample(request)).Returns(sample);
            sampleRepository.Setup(x => x.Add(sample)).Returns(addResult);

            // Act
            var result = service.Add(request);

            // Assert
            result.IsSuccess.Should().BeFalse();
        }

        [Test]
        public void AddSample_AddingSucceeds_ReturnSuccessResult()
        {
            // Arrange
            var request = new AddSampleRequest();
            var sample = new Sample();
            const bool addResult = true;

            sampleFactory.Setup(x => x.CreateAddSample(request)).Returns(sample);
            sampleRepository.Setup(x => x.Add(sample)).Returns(addResult);

            // Act
            var result = service.Add(request);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Test]
        public void UpdateSample_UpdatingFails_ReturnErrorResult()
        {
            // Arrange
            var request = new UpdateSampleRequest {Id = 1};
            var sample = new Sample
            {
                Id = 1,
                CreatedDate = It.IsAny<DateTime>()
            };
            const bool updateResult = false;

            sampleRepository.Setup(x => x.Get(s => s.Id == request.Id)).Returns(sample);
            sampleFactory.Setup(x => x.CreateUpdateSample(request, sample.CreatedDate)).Returns(sample);
            sampleRepository.Setup(x => x.Update(sample)).Returns(updateResult);

            // Act
            var result = service.Update(request);

            // Assert
            result.IsSuccess.Should().BeFalse();
        }

        [Test]
        public void UpdateSample_UpdatingSucceeds_ReturnSuccessResult()
        {
            // Arrange
            var request = new UpdateSampleRequest {Id = 1};
            var sample = new Sample
            {
                Id = 1,
                CreatedDate = It.IsAny<DateTime>()
            };
            const bool updateResult = true;

            sampleRepository.Setup(x => x.Get(s => s.Id == request.Id)).Returns(sample);
            sampleFactory.Setup(x => x.CreateUpdateSample(request, sample.CreatedDate)).Returns(sample);
            sampleRepository.Setup(x => x.Update(sample)).Returns(updateResult);

            // Act
            var result = service.Update(request);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}