using Kravets.Chatter.DAL.Infrastructure;
using Kravets.Chatter.DAL.Infrastructure.Attributes;

namespace Kravets.Chatter.DAL.StoredProcedures.Messages
{
    [ProcedureName("spUpdateMessage")]
    internal class spUpdateMessage : StoredProcedure
    {
        [InParameter] public long Id;
        [InParameter] public string Text;
    }
}
