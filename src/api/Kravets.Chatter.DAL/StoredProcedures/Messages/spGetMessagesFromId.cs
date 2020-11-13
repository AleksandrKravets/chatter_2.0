using Kravets.Chatter.DAL.Infrastructure;
using Kravets.Chatter.DAL.Infrastructure.Attributes;

namespace Kravets.Chatter.DAL.StoredProcedures.Messages
{
    [ProcedureName("spGetMessagesFromId")]
    internal class spGetMessagesFromId : StoredProcedure
    {
        [InParameter] public long Id;
    }
}
