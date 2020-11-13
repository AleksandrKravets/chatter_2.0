using System;

namespace Kravets.Chatter.API.BackgroundServices.Models.DelayedMessageDeletion
{
    public class DelayedMessageDeletingTask
    {
        public Guid TaskId { get; private set; }
        public long UserId { get; private set; }
        public long MessageId { get; private set; }
        public ActionType Type { get; private set; }

        public DelayedMessageDeletingTask(Guid taskId, long messageId, long userId, ActionType type)
        {
            MessageId = messageId;
            UserId = userId;
            Type = type;
            TaskId = taskId;
        }
    }
}
