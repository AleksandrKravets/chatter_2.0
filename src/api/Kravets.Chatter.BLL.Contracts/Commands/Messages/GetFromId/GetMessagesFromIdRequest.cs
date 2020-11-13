using MediatR;
using System.Collections.Generic;

namespace Kravets.Chatter.BLL.Contracts.Commands.Messages.GetFromId
{
    /// <summary>
    /// It is a request for receiving messages with Id greater than the Id from parameters.
    /// </summary>
    public class GetMessagesFromIdRequest : IRequest<IEnumerable<MessageModel>>
    {
        /// <summary>
        /// Message identifier.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="id">Message identifier.</param>
        public GetMessagesFromIdRequest(long id)
        {
            Id = id;
        }
    }
}
