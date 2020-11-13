using Kravets.Chatter.DAL.Infrastructure;
using Kravets.Chatter.DAL.Infrastructure.Attributes;

namespace Kravets.Chatter.DAL.StoredProcedures.SavedMessages
{
    [ProcedureName("spCreateSavedMessage")]
    internal class spCreateSavedMessage : StoredProcedure
    {
        [InParameter] public long UserId;
        [InParameter] public long MessageId;
    }
}