using System;
using Microsoft.Extensions.Logging;
using Moq;

namespace WebApp.Tests.Helpers
{
    public static class AssertHelpers
    {
        public static void VerifyLogger<T>(Mock<ILogger<T>> logger, LogLevel logLevel, Times times) where T: class
        {
            logger.Verify(x => x.Log(
                logLevel,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), times);
        }
    }
}