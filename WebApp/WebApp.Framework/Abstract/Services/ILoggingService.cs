using System;

namespace WebApp.Framework.Abstract
{
    public interface ILoggingService
    {
        void LogError(string methodName, string message, Exception ex);
        void LogError(string methodName, string message);
        void LogWarn(string methodName, string message);
        void LogInfo(string methodName, string message);
        void LogDebug(string methodName, string message);
    }
}