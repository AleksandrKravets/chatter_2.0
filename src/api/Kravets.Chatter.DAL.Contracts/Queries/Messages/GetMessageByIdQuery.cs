using System;

namespace Kravets.Chatter.DAL.Contracts.Queries.Messages
{
    /// <summary>
    /// Represents query to get a message by identifier.
    /// </summary>
    public class GetMessageByIdQuery
    {
        /// <summary>
        /// Query parameters.
        /// </summary>
        public class Parameters
        {
            /// <summary>
            /// Message identifier.
            /// </summary>
            public long Id { get; private set; }

            /// <summary>
            /// Initializes instance.
            /// </summary>
            /// <param name="id">Message identifier.</param>
            public Parameters(long id) =>
                (Id) = (id);
        }

        /// <summary>
        /// Query result.
        /// </summary>
        public class Result
        {
            /// <summary>
            /// Message identifier.
            /// </summary>
            public long Id { get; set; }
            /// <summary>
            /// Message text.
            /// </summary>
            public string Text { get; set; }
            /// <summary>
            /// Time of message creation.
            /// </summary>
            public DateTime CreationTime { get; set; }
            /// <summary>
            /// Boolean flag that shows if message is updated.
            /// </summary>
            public bool IsUpdated { get; set; }
            /// <summary>
            /// Message owner identifier.
            /// </summary>
            public long UserId { get; set; }
            /// <summary>
            /// Message owner nickname.
            /// </summary>
            public string UserNickName { get; set; }
            /// <summary>
            /// Boolean flag that shows if message is reply.
            /// </summary>
            public bool IsReply { get; set; }
            /// <summary>
            /// Message to reply text.
            /// </summary>
            public string MessageToReplyText { get; set; }
            /// <summary>
            /// Message to reply owner identifier.
            /// </summary>
            public long? MessageToReplyOwnerId { get; set; }
            /// <summary>
            /// Message to reply owner nickname.
            /// </summary>
            public string MessageToReplyOwnerNickname { get; set; }
        }
    }
}
