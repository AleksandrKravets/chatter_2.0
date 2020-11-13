using Kravets.Chatter.DAL.Infrastructure;
using Kravets.Chatter.DAL.Infrastructure.Attributes;

namespace Kravets.Chatter.DAL.StoredProcedures.Mutes
{
    [ProcedureName("spGetMutes")]
    internal class spGetMutes : StoredProcedure
    {
        [InParameter] public long UserId;
    }
}
