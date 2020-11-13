using Kravets.Chatter.DAL.Infrastructure;
using Kravets.Chatter.DAL.Infrastructure.Attributes;

namespace Kravets.Chatter.DAL.StoredProcedures.SavedMessages
{
    [ProcedureName("spGetSavedMessage")]
    internal class spGetSavedMessage : StoredProcedure
    {
        [InParameter] public long Id;
    }
}
