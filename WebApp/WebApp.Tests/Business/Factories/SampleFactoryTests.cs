using FluentAssertions;
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
            var request = new AddSampleRequest();
            
            // Act
            var result = factory.CreateAddSample(request);

            // Assert
            result.Title.Should().Be(request.Title);
        }
    }
}