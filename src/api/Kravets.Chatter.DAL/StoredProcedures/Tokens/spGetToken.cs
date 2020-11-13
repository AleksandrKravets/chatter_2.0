using Kravets.Chatter.DAL.Infrastructure;
using Kravets.Chatter.DAL.Infrastructure.Attributes;

namespace Kravets.Chatter.DAL.StoredProcedures.Tokens
{
    [ProcedureName("spGetToken")]
    internal class spGetToken : StoredProcedure
    {
        [InParameter] public string Token;
    }
}
