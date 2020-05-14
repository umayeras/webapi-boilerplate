using System;
using Microsoft.Extensions.Logging;
using WebApp.Framework.Abstract;

namespace WebApp.Framework.Services
{
    public class LoggingService : ILoggingService
    {
        private readonly ILogger logger;
        
        public LoggingService(ILogger<LoggingService> logger)
        {
            this.logger = logger;
        }

        private static string CreateMessage(string methodName, string message)
        {
            return $"({methodName}) - {message}";
        }
        
        public void LogError(string methodName, string message, Exception ex)
        {
            var errorMessage = CreateMessage(methodName, message);
            logger.LogError(errorMessage, ex);
        }

        public void LogError(string methodName, string message)
        {
            var errorMessage = CreateMessage(methodName, message);
            logger.LogDebug(errorMessage);
        }

        public void LogWarn(string methodName, string message)
        {
            var warnMessage = CreateMessage(methodName, message);
            logger.LogWarning(warnMessage);
        }

        public void LogInfo(string methodName, string message)
        {
            var infoMessage = CreateMessage(methodName, message);
            logger.LogInformation(infoMessage);
        }

        public void LogDebug(string methodName, string message)
        {
            var debugMessage = CreateMessage(methodName, message);
            logger.LogDebug(debugMessage);
        }
    }
}