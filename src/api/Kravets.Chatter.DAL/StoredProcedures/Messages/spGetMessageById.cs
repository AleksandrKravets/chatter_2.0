using Kravets.Chatter.DAL.Infrastructure;
using Kravets.Chatter.DAL.Infrastructure.Attributes;

namespace Kravets.Chatter.DAL.StoredProcedures.Messages
{
    [ProcedureName("spGetMessageById")]
    internal class spGetMessageById : StoredProcedure
    {
        [InParameter] public long Id;
    }
}
