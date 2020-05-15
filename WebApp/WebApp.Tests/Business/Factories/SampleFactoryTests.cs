using System;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using WebApp.Business.Factories;
using WebApp.Model;

namespace WebApp.Tests.Business.Factories
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class SampleFactoryTests
    {
        #region members & setup

        private SampleFactory factory;

        [SetUp]
        public void Init()
        {
            factory = new SampleFactory();
        }

        #endregion

        [Test]
        public void CreateAddSample_NoCondition_ReturnSample()
        {
            // Arrange
            var request = new AddSampleRequest
            {
                Title = "Sample Title"
            };
            
            // Act
            var result = factory.CreateAddSample(request);

            // Assert
            result.Title.Should().Be(request.Title);
            result.CreatedDate.Should().BeCloseTo(DateTime.Now, 100, "dates should differ at milliseconds level");
            result.ModifiedDate.Should().BeCloseTo(DateTime.Now, 100, "dates should differ at milliseconds level");
            result.Status.Should().Be(0);
        }
    }
}