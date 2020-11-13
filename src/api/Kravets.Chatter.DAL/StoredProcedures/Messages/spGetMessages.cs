using Kravets.Chatter.DAL.Infrastructure;
using Kravets.Chatter.DAL.Infrastructure.Attributes;

namespace Kravets.Chatter.DAL.StoredProcedures.Messages
{
    [ProcedureName("spGetMessages")]
    internal class spGetMessages : StoredProcedure
    {
        [InParameter] public long? LastMessageId;
        [InParameter] public int PageSize;
        [OutParameter] public bool NextPageExists;
    }
}
