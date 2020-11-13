using Kravets.Chatter.DAL.Infrastructure;
using Kravets.Chatter.DAL.Infrastructure.Attributes;

namespace Kravets.Chatter.DAL.StoredProcedures.Users
{
    [ProcedureName("spCreateUser")]
    internal class spCreateUser : StoredProcedure
    {
        [InParameter] public string Nickname;
        [InParameter] public string HashedPassword;
    }
}
