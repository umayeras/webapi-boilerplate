using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using WebApp.Business.Services;
using WebApp.Data.Repositories;
using WebApp.Data.Uow;
using WebApp.Model.Entities;

namespace WebApp.Tests.Business.Services
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class StatusServiceTests
    {
        #region members & setup

        private StatusService service;

        private Mock<ILogger<StatusService>> logger;
        private Mock<IUnitOfWork> unitOfWork;
        private Mock<IBaseRepository<Status>> repository;

        [SetUp]
        public void Init()
        {
            logger = new Mock<ILogger<StatusService>>();
            unitOfWork = new Mock<IUnitOfWork>();
            repository = new Mock<IBaseRepository<Status>>();
            unitOfWork.Setup(x => x.GetRepository<Status>()).Returns(repository.Object);

            service = new StatusService(unitOfWork.Object, logger.Object);
        }

        #endregion

        [Test]
        public async Task GetAll_ItemsNotInCacheAlready_ReturnList()
        {
            // Arrange
            var item1 = new Status {Id = 1, Name = "Active"};
            var item2 = new Status {Id = 2, Name = "Passive"};
            var list = new List<Status> {item1, item2}.AsQueryable();

            repository.Setup(x => x.GetAllAsync(null)).ReturnsAsync(list);

            // Act
            var result = await service.GetAll();

            // Assert
            result.Should().BeEquivalentTo(list);

            var firstItem = result.FirstOrDefault();
            firstItem.Id.Should().Be(list.FirstOrDefault().Id);
            firstItem.Name.Should().Be(list.FirstOrDefault().Name);
        }
    }
}