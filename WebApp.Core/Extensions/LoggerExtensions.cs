using System;

namespace WebApp.Core.Extensions
{
    public static class LoggerExtensions
    {
        public static string CreateLogMessage(this Type type, string methodName, string message)
        {
            return $"{type.Name}.{methodName},{type.Name} {methodName} failed. ResultMessage: {message}";
        }
    }
}