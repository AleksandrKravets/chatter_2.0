using Kravets.Chatter.DAL.Infrastructure;
using Kravets.Chatter.DAL.Infrastructure.Attributes;
using System;

namespace Kravets.Chatter.DAL.StoredProcedures.Messages
{
    [ProcedureName("spCreateMessage")]
    internal class spCreateMessage : StoredProcedure
    {
        [InParameter] public string Text;
        [InParameter] public DateTime CreationTime;
        [InParameter] public bool IsReply;
        [InParameter] public long UserId;
        [InParameter] public long? MessageToReplyId;
        [OutParameter] public long NewMessageId;
    }
}
