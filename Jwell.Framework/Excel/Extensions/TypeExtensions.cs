using System;
using System.Collections.Generic;
using System.Reflection;

namespace Jwell.Framework.Excel
{
    internal static class TypeExtensions
    {
        public static Type UnwrapNullableType(this Type type) => Nullable.GetUnderlyingType(type) ?? type;

        public static bool IsPrimitive(this Type type) => type.IsInteger() || type.IsNonIntegerPrimitive();

        public static bool IsInteger(this Type type)
        {
            type = type.UnwrapNullableType();

            return (type == typeof(int))
                   || (type == typeof(long))
                   || (type == typeof(short))
                   || (type == typeof(byte))
                   || (type == typeof(uint))
                   || (type == typeof(ulong))
                   || (type == typeof(ushort))
                   || (type == typeof(sbyte))
                   || (type == typeof(char));
        }

        public static object GetDefaultValue(this Type type)
        {
            if (!type.GetTypeInfo().IsValueType)
            {
                return null;
            }
            object value;
            return _commonTypeDictionary.TryGetValue(type, out value)? value: Activator.CreateInstance(type);
        }

        private static bool IsNonIntegerPrimitive(this Type type)
        {
            type = type.UnwrapNullableType();

            return (type == typeof(bool))
                   || (type == typeof(byte[]))
                   || (type == typeof(DateTime))
                   || (type == typeof(TimeSpan))
                   || (type == typeof(DateTimeOffset))
                   || (type == typeof(decimal))
                   || (type == typeof(double))
                   || (type == typeof(float))
                   || (type == typeof(Guid))
                   || (type == typeof(string))
                   || type.GetTypeInfo().IsEnum;
        }

        private static readonly Dictionary<Type, object> _commonTypeDictionary = new Dictionary<Type, object>
        {
            { typeof(Guid), default(Guid) },
            { typeof(TimeSpan), default(TimeSpan) },
            { typeof(DateTime), default(DateTime) },
            { typeof(DateTimeOffset), default(DateTimeOffset) },
            { typeof(char), default(char) },
            { typeof(int), default(int) },
            { typeof(uint), default(uint) },
            { typeof(long), default(long) },
            { typeof(ulong), default(ulong) },
            { typeof(short), default(short) },
            { typeof(ushort), default(ushort) },
            { typeof(byte), default(byte) },
            { typeof(sbyte), default(sbyte) },
            { typeof(bool), default(bool) },
            { typeof(double), default(double) },
            { typeof(float), default(float) },
        };
    }
}
