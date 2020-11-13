using System;
using System.Collections.Generic;

namespace Kravets.Chatter.DAL.Contracts.Queries.Messages
{
    /// <summary>
    /// Represents query to get a collection of messages.
    /// </summary>
    public class GetMessagesQuery
    {
        /// <summary>
        /// Query parameters.
        /// </summary>
        public class Parameters
        {
            /// <summary>
            /// Last message identifier.
            /// </summary>
            public long? LastMessageId { get; private set; }
            /// <summary>
            /// Page size.
            /// </summary>
            public int PageSize { get; private set; }

            /// <summary>
            /// Initializes instance.
            /// </summary>
            /// <param name="lastMessageId">Last message identifier.</param>
            /// <param name="pageSize">Page size.</param>
            public Parameters(long? lastMessageId, int pageSize) =>
                (LastMessageId, PageSize) = (lastMessageId, pageSize);
        }

        /// <summary>
        /// Result DTO.
        /// </summary>
        public class ResultDto
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

        /// <summary>
        /// Query result.
        /// </summary>
        public class Result
        {
            /// <summary>
            /// The collection of messages.
            /// </summary>
            public IEnumerable<ResultDto> Messages { get; private set; }
            /// <summary>
            /// Flag that shows if next page exists.
            /// </summary>
            public bool NextPageExists { get; private set; }

            /// <summary>
            /// Initializes instance.
            /// </summary>
            /// <param name="messages">The collection of messages.</param>
            /// <param name="nextPageExists">Flag that shows if next page exists.</param>
            public Result(IEnumerable<ResultDto> messages, bool nextPageExists) =>
                (Messages, NextPageExists) = (messages, nextPageExists);
        }
    }
}
