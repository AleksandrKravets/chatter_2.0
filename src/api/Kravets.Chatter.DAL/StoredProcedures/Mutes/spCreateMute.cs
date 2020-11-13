using Kravets.Chatter.DAL.Infrastructure;
using Kravets.Chatter.DAL.Infrastructure.Attributes;

namespace Kravets.Chatter.DAL.StoredProcedures.Mutes
{
    [ProcedureName("spCreateMute")]
    internal class spCreateMute : StoredProcedure
    {
        [InParameter] public long UserId;
        [InParameter] public long UserToMuteId;
    }
}
