using Kravets.Chatter.DAL.Infrastructure;
using Kravets.Chatter.DAL.Infrastructure.Attributes;

namespace Kravets.Chatter.DAL.StoredProcedures.SavedMessages
{
    [ProcedureName("spGetSavedMessages")]
    internal class spGetSavedMessages : StoredProcedure
    {
        [InParameter] public long UserId;
        [InParameter] public long PageIndex;
        [InParameter] public long PageSize;
        [OutParameter] public bool NextPageExists;
    }
}