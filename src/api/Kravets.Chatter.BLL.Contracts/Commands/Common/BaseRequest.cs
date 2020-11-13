using Kravets.Chatter.BLL.Contracts.Models.Responses;
using MediatR;
using OneOf;
using OneOf.Types;
using System.Threading;
using System.Threading.Tasks;

namespace Kravets.Chatter.BLL.Contracts.Commands.Common
{
    /// <summary>
    /// Represents base request.
    /// </summary>r
    public class BaseRequest
    {
        /// <summary>
        /// User identifier.
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Initializes instance.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        public BaseRequest(long userId)
        {
            UserId = userId;
        }

        /// <summary>
        /// Initializes instance.
        /// </summary>
        public BaseRequest()
        {

        }
    }

    public class SomeRequest : BaseRequest, IRequest<long>
    {
        public SomeRequest()
        {

        }
    }

    // проверять продолжительность запроса таким пайпом и логировать
    // services.AddScoped(typeof(IPipelineBehavior<,>, typeof(UserIdPipe<TIn, TOut>))
    // пайпы выполняются в порядке их регистрации
    public class UserIdPipe<TIn, TOut> : IPipelineBehavior<TIn, TOut>
    {
        // inject context using IHttpContextAccessor.

        public UserIdPipe()
        {

        }

        public async Task<TOut> Handle(TIn request, CancellationToken cancellationToken, RequestHandlerDelegate<TOut> next)
        {
            var userId = 1;

            if(request is BaseRequest br)
            {
                br.UserId = userId;
            }

            return await next();
        }
    }

    public interface IRequestWrapper<T> : IRequest<OneOf<Success<T>, BLError>> { }

    public interface IHandlerWrapper<TIn, TOut> : IRequestHandler<TIn, OneOf<Success<TOut>, BLError>>
        where TIn : IRequestWrapper<TOut>
    {
    }
}
