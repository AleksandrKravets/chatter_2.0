using Kravets.Chatter.DAL.Infrastructure;
using Kravets.Chatter.DAL.Infrastructure.Attributes;

namespace Kravets.Chatter.DAL.StoredProcedures.Messages
{
    [ProcedureName("spDeleteMessage")]
    internal class spDeleteMessage : StoredProcedure
    {
        [InParameter] public long Id;
    }
}
