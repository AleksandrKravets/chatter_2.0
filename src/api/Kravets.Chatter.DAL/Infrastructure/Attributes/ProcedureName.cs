using System;

namespace Kravets.Chatter.DAL.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    internal class ProcedureName : Attribute
    {
        public string Name { get; private set; }

        public ProcedureName(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException(nameof(name));

            Name = name;
        }
    }
}
