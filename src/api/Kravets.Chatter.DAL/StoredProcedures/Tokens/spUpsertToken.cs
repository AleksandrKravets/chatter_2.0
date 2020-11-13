using Kravets.Chatter.DAL.Infrastructure;
using Kravets.Chatter.DAL.Infrastructure.Attributes;
using System;

namespace Kravets.Chatter.DAL.StoredProcedures.Tokens
{
    [ProcedureName("spUpsertToken")]
    internal class spUpsertToken : StoredProcedure
    {
        [InParameter] public string JwtId;
        [InParameter] public DateTime CreationTime;
        [InParameter] public DateTime ExpiryTime;
        [InParameter] public string Token;
        [InParameter] public long UserId;
    }
}
