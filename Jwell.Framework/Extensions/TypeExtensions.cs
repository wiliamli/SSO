using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Jwell.Framework.Extensions
{
    public static class TypeExtensions
    {
        public static TAttribute GetFirstAttribute<TAttribute>(this Type type, bool includeInterfaceAttributes = true)
            where TAttribute : Attribute
        {
            IEnumerable<TAttribute> classAttributes = type.GetCustomAttributes<TAttribute>();

            IEnumerable<TAttribute> interfaceAttributes = Enumerable.Empty<TAttribute>();
            if (includeInterfaceAttributes)
            {
                interfaceAttributes = type.GetInterfaceAttributes<TAttribute>();
            }

            return classAttributes.Union(interfaceAttributes).FirstOrDefault();
        }

        public static IEnumerable<TAttribute> GetInterfaceAttributes<TAttribute>(this Type type)
            where TAttribute : Attribute
        {
            return type.GetInterfaces().SelectMany(t => t.GetTypeInfo().GetCustomAttributes<TAttribute>()); ;
        }

        public static bool GenericEq(this Type type, Type toCompare)
        {
            return type.Namespace == toCompare.Namespace && type.Name == toCompare.Name;
        }
    }
}
