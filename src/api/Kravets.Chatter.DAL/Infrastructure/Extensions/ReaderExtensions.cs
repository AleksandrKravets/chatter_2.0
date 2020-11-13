using System;
using System.Collections.Generic;
using System.Data.Common;

namespace Kravets.Chatter.DAL.Infrastructure.Extensions
{
    internal static class ReaderExtensions
    {
        public static T ReadObject<T>(this DbDataReader reader)
        {
            reader.Read();

            var instance = (T)Activator.CreateInstance(typeof(T));

            foreach (var instanceProperty in instance.GetType().GetProperties())
            {
                instanceProperty.SetValue(instance, Convert.ChangeType(reader[instanceProperty.Name], instanceProperty.PropertyType));
            }

            return instance;
        }

        public static IEnumerable<T> ReadList<T>(this DbDataReader reader)
        {
            while (reader.Read())
                yield return reader.ReadObject<T>();
        }
    }
}
