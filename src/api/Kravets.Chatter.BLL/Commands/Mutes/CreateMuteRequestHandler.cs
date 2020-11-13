using Kravets.Chatter.BLL.Contracts.Commands.Mutes.Create;
using Kravets.Chatter.BLL.Contracts.Models.Responses;
using Kravets.Chatter.Common.ResponseMessages;
using Kravets.Chatter.DAL.Contracts.Queries.Mutes;
using Kravets.Chatter.DAL.Contracts.Repositories;
using MediatR;
using OneOf;
using OneOf.Types;
using System.Threading;
using System.Threading.Tasks;

namespace Kravets.Chatter.BLL.Commands.Mutes
{
    /// <summary>
    /// Represents handler to mute user by identifier.
    /// </summary>
    public class CreateMuteRequestHandler : IRequestHandler<CreateMuteRequest, OneOf<Success, BLError>>
    {
        private readonly IMutesRepository _mutesRepository;

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="mutesRepository">Mutes repository provider.</param>
        public CreateMuteRequestHandler(IMutesRepository mutesRepository)
        {
            _mutesRepository = mutesRepository;
        }

        /// <summary>
        /// Handles request.
        /// </summary>
        /// <param name="request">Request payload.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Success or BL error.</returns>
        public async Task<OneOf<Success, BLError>> Handle(CreateMuteRequest request, CancellationToken cancellationToken)
        {
            if(request.UserId == request.UserToMuteId) return new BLError(ErrorMessages.YouCantMuteYouself);

            await _mutesRepository.CreateAsync(
                new CreateMuteQuery.Parameters(request.UserId, request.UserToMuteId), cancellationToken);

            return new Success();
        }
    }
}