using Kravets.Chatter.DAL.Infrastructure;
using Kravets.Chatter.DAL.Infrastructure.Attributes;

namespace Kravets.Chatter.DAL.StoredProcedures.SavedMessages
{
    [ProcedureName("spDeleteSavedMessage")]
    internal class spDeleteSavedMessage : StoredProcedure
    {
        [InParameter] public long Id;
    }
}
