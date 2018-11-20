using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Framework.Aspect
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = true)]
    public class InterceptAttribute : Attribute
    {
        public Type[] InterceptBy { get; private set; }

        public InterceptAttribute(params Type[] interceptBy)
        {
            if (interceptBy == null)
            {
                throw new ArgumentNullException(nameof(interceptBy));
            }

            InterceptBy = interceptBy;
        }
    }
}
