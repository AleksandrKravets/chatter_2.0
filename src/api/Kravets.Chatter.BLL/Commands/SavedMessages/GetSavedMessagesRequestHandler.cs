using Kravets.Chatter.BLL.Contracts.Commands.SavedMessages.GetList;
using Kravets.Chatter.BLL.Contracts.Models.Collections;
using Kravets.Chatter.DAL.Contracts.Queries.SavedMessages;
using Kravets.Chatter.DAL.Contracts.Repositories;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kravets.Chatter.BLL.Commands.SavedMessages
{
    /// <summary>
    /// Represents request to get saved message.
    /// </summary>
    public class GetSavedMessagesRequestHandler : IRequestHandler<GetSavedMessagesRequest, PagedListModel<SavedMessageModel>>
    {
        private readonly ISavedMessagesRepository _savedMessagesRepository;

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="savedMessagesRepository">Saved messages repository provider.</param>
        public GetSavedMessagesRequestHandler(ISavedMessagesRepository savedMessagesRepository)
        {
            _savedMessagesRepository = savedMessagesRepository;
        }

        /// <summary>
        /// Handles request.
        /// </summary>
        /// <param name="request">Request payload.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The collection of <see cref="SavedMessageModel"></see>.</returns>
        public async Task<PagedListModel<SavedMessageModel>> Handle(GetSavedMessagesRequest request, CancellationToken cancellationToken)
        {
            var response = await _savedMessagesRepository.GetAsync(
                new GetSavedMessagesQuery.Parameters(request.UserId, request.PageIndex, request.PageSize), cancellationToken);

            var savedMessages = response.Messages.Select(x => new SavedMessageModel(x, request.UserId));

            var result = new PagedListModel<SavedMessageModel>(savedMessages, request.PageIndex, request.PageSize, response.NextPageExists);

            return result;
        }
    }
}
