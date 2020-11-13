using Kravets.Chatter.DAL.Contracts.Queries.SavedMessages;
using System;

namespace Kravets.Chatter.BLL.Contracts.Commands.SavedMessages.GetList
{
    /// <summary>
    /// Represents saved message model.
    /// </summary>
    public class SavedMessageModel
    {
        /// <summary>
        /// Saved message identifier.
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Saved message model.
        /// </summary>
        public MessageModel Message { get; set; }

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="dto">Message dto.</param>
        /// <param name="loggedUserId">Current logged user identifier.</param>
        public SavedMessageModel(GetSavedMessagesQuery.ResultDto dto, long loggedUserId)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            Id = dto.Id;
            Message = new MessageModel(dto, loggedUserId);
        }
    }
}
