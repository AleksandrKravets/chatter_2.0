using Kravets.Chatter.BLL.Contracts.Models.Responses;
using OneOf;
using OneOf.Types;

namespace Kravets.Chatter.BLL.Extensions
{
    public static class OneOfExtensions
    {
        public static bool IsSuccess<T>(this OneOf<Success<T>, BLError> oneOf) => oneOf.IsT0;
        public static bool IsSuccess(this OneOf<Success, BLError> oneOf) => oneOf.IsT0;
    }
}
