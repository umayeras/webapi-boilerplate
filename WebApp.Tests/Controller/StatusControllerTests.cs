using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using WebApp.Business.Abstract.Services;
using WebApp.Controllers;
using WebApp.Model.Entities;
using WebApp.Model.Requests;
using WebApp.Model.Results;
using WebApp.Validation;
using WebApp.Validation.Abstract;

namespace WebApp.Tests.Controller
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class StatusControllerTests
    {
        #region members & setup

        private StatusController controller;
        private Mock<IStatusService> statusService;

        [SetUp]
        public void Init()
        {
            statusService = new Mock<IStatusService>();

            controller = new StatusController(statusService.Object);
        }

        #endregion

        [Test]
        public async Task Get_NoCondition_ReturnList()
        {
            // Arrange
            var statusList = new List<Status>();
            var apiResult = new OkObjectResult(statusList);

            statusService.Setup(x => x.GetAll()).ReturnsAsync(statusList);

            // Act
            var result = await controller.Get();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            result.Should().BeEquivalentTo(apiResult);
        }
    }
}