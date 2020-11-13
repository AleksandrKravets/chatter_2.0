using Kravets.Chatter.DAL.Infrastructure;
using Kravets.Chatter.DAL.Infrastructure.Attributes;

namespace Kravets.Chatter.DAL.StoredProcedures.Mutes
{
    [ProcedureName("spDeleteMute")]
    internal class spDeleteMute : StoredProcedure
    {
        [InParameter] public long UserId;
        [InParameter] public long UserToUnmuteId;
    }
}
