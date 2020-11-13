using Kravets.Chatter.DAL.Infrastructure;
using Kravets.Chatter.DAL.Infrastructure.Attributes;

namespace Kravets.Chatter.DAL.StoredProcedures.Users
{
    [ProcedureName("spGetUserByNickname")]
    internal class spGetUserByNickname : StoredProcedure
    {
        [InParameter] public string Nickname;
    }
}
