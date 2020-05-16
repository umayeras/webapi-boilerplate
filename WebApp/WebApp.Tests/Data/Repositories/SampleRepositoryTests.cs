using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using WebApp.Data;
using WebApp.Data.Abstract;
using WebApp.Data.Repositories;
using WebApp.Framework.Abstract;
using WebApp.Model;

namespace WebApp.Tests.Data.Repositories
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class SampleRepositoryTests
    {
        #region members & setup

        private SampleRepository repository;
        private Mock<ILoggingService> logger;
        private Mock<IBaseRepository<Sample>> baseRepository;
        private Mock<WebAppDbContext> context;

        [SetUp]
        public void Init()
        {
            context = new Mock<WebAppDbContext>();
            baseRepository = new Mock<IBaseRepository<Sample>>();
            logger = new Mock<ILoggingService>();

            repository = new SampleRepository(logger.Object);
        }

        #endregion

        #region get

        [Test]
        public void GetListAsync_NoCondition_ReturnList()
        {
            // Arrange

            // Act
            var result = repository.GetListAsync();

            // Assert
            result.Result.Should().BeOfType<List<Sample>>();
        }

        [Test]
        public void GetList_NoCondition_ReturnList()
        {
            // Arrange

            // Act
            var result = repository.GetList();

            // Assert
            result.Should().BeOfType<List<Sample>>();
        }

        [Test]
        public void GetAsync_NoCondition_ReturnItem()
        {
            // Arrange
            var sample = new Sample {Id = 1};
            Expression<Func<Sample, bool>> filter = n => true;
            baseRepository.Setup(x => x.Get(filter)).Returns(sample);

            // Act
            var result = repository.GetAsync(x => x.Id == sample.Id);

            // Assert
            result.Result.Should().BeOfType<Sample>();
            result.Result.Id.Should().Be(sample.Id);
        }

        [Test]
        public void Get_NoCondition_ReturnItem()
        {
            // Arrange
            var sample = new Sample {Id = 1};
            Expression<Func<Sample, bool>> filter = n => true;
            baseRepository.Setup(x => x.Get(filter)).Returns(sample);

            // Act
            var result = repository.Get(x => x.Id == sample.Id);

            // Assert
            result.Should().BeOfType<Sample>();
            result.Id.Should().Be(sample.Id);
        }

        #endregion

        #region add

        [Test]
        public void Add_AddingFails_ReturnFalse()
        {
            // Arrange
           
            // Act

            // Assert
        }
        
        [Test]
        public void Add_AddingSucceeds_ReturnTrue()
        {
            // Arrange
           
            // Act

            // Assert
        }

        #endregion

        #region update

        [Test]
        public void Update_UpdatingFails_ReturnFalse()
        {
            // Arrange
           
            // Act

            // Assert
        }
        
        [Test]
        public void Update_UpdatingSucceeds_ReturnTrue()
        {
            // Arrange
           
            // Act

            // Assert
        }

        #endregion

        #region delete

        [Test]
        public void Delete_DeletingFails_ReturnFalse()
        {
            // Arrange
           
            // Act

            // Assert
        }
        
        [Test]
        public void Delete_DeletingSucceeds_ReturnTrue()
        {
            // Arrange
           
            // Act

            // Assert
        }

        #endregion
    }
}