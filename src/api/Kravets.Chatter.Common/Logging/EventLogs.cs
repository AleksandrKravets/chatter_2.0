using Microsoft.Extensions.Logging;

namespace Kravets.Chatter.Common.Logging
{
    public static class EventLogs
    {
        public static readonly EventId BLError = new EventId(9000, "BLError");
        public static readonly EventId BackgroundCritical = new EventId(9001, "BackgroundCritical");

    }
}
