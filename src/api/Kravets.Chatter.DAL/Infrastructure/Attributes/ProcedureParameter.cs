using System;

namespace Kravets.Chatter.DAL.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    internal abstract class ProcedureParameter : Attribute
    {
    }
}
