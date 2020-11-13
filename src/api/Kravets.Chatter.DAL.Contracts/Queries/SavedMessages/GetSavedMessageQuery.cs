using System;

namespace Kravets.Chatter.DAL.Contracts.Queries.SavedMessages
{
    /// <summary>
    /// Represents query to get saved message by identifier.
    /// </summary>
    public class GetSavedMessageQuery
    {
        /// <summary>
        /// Parameters.
        /// </summary>
        public class Parameters
        {
            /// <summary>
            /// Saved message identifier.
            /// </summary>
            public long Id { get; private set; }

            /// <summary>
            /// Initializes instance.
            /// </summary>
            /// <param name="id">Saved message identifier.</param>
            public Parameters(long id)
            {
                Id = id;
            }
        }

        /// <summary>
        /// Result DTO.
        /// </summary>
        public class Result
        {
            /// <summary>
            /// Saved message identifier.
            /// </summary>
            public long Id { get; set; }
            /// <summary>
            /// User that saved message identifier.
            /// </summary>
            public long SavedMessageOwnerId { get; set; }
            /// <summary>
            /// Message identifier.
            /// </summary>
            public long MessageId { get; set; }
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
