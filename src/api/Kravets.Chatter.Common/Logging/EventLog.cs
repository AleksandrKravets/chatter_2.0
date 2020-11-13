using Microsoft.Extensions.Logging;
using System;

namespace Kravets.Chatter.Common.Logging
{
    public static class EventLog
    {
        public static void BLError(ILogger logger, string message)
        {
            BLErrorExecute(logger, message, DateTime.Now.ToString("hh:mm:ss"), null);
        }

        public static void BackgroundCritical(ILogger logger, string message, Exception exception)
        {
            BackgroundCriticalExecute(logger, message, DateTime.Now.ToString("hh:mm:ss"), exception);
        }

        private static readonly Action<ILogger, string, string, Exception> BLErrorExecute =
            LoggerMessage.Define<string, string>(LogLevel.Information, EventLogs.BLError, "BL Error. {1} at {2}.");

        private static readonly Action<ILogger, string, string, Exception> BackgroundCriticalExecute =
            LoggerMessage.Define<string, string>(LogLevel.Critical, EventLogs.BackgroundCritical, "Fatal error in background service. {1} at {2}.");
    }
}
