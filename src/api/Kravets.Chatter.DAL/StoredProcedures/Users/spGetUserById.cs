using Kravets.Chatter.DAL.Infrastructure;
using Kravets.Chatter.DAL.Infrastructure.Attributes;

namespace Kravets.Chatter.DAL.StoredProcedures.Users
{
    [ProcedureName("spGetUserById")]
    internal class spGetUserById : StoredProcedure
    {
        [InParameter] public long Id;
    }
}
