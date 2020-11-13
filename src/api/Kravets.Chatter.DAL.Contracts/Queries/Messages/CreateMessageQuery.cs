using System;

namespace Kravets.Chatter.DAL.Contracts.Queries.Messages
{
    /// <summary>
    /// Represents query to create message.
    /// </summary>
    public class CreateMessageQuery
    {
        /// <summary>
        /// Query parameters.
        /// </summary>
        public class Parameters
        {
            /// <summary>
            /// Message creator identifier.
            /// </summary>
            public long UserId { get; private set; }
            /// <summary>
            /// Message text.
            /// </summary>
            public string Text { get; private set; }
            /// <summary>
            /// Message creation time.
            /// </summary>
            public DateTime CreationTime { get; private set; }
            /// <summary>
            /// Is message reply.
            /// </summary>
            public bool IsReply { get; private set; }
            /// <summary>
            /// Message to reply identifier.
            /// </summary>
            public long? MessageToReplyId { get; private set; }

            /// <summary>
            /// Initializes instance.
            /// </summary>
            /// <param name="userId">User identifier.</param>
            /// <param name="text">Message text.</param>
            /// <param name="creationTime">Time of message creation.</param>
            /// <param name="isReply">Is message reply.</param>
            /// <param name="messageToReplyId">Message to reply identifier.</param>
            public Parameters(long userId, string text, DateTime creationTime, bool isReply, long? messageToReplyId) =>
                (UserId, Text, CreationTime, IsReply, MessageToReplyId) = (userId, text, creationTime, isReply, messageToReplyId);
        }

        /// <summary>
        /// Represents query result.
        /// </summary>
        public class Result
        {
            /// <summary>
            /// Created message identifier.
            /// </summary>
            public long CreatedMessageId { get; set; }

            /// <summary>
            /// Initializes instance.
            /// </summary>
            /// <param name="createdMessageId">Created message identifier.</param>
            public Result(long createdMessageId) => (CreatedMessageId) = (createdMessageId);
        }
    }
}
