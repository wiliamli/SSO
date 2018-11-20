using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Jwell.Framework.Modules
{
    public class JwellModuleInfo
    {
        public Assembly Assembly { get; }

        public Type Type { get; }

        public JwellModule Instance { get; internal set; }

        public List<JwellModuleInfo> Dependencies { get; }

        private int _order;

        public int Order
        {
            get
            {
                return _order;
            }
            set
            {
                if (value > _order)
                {
                    _order = value;
                }
            }
        }

        public JwellModuleInfo(Type type)
        {
            Type = type;
            Assembly = type.GetTypeInfo().Assembly;
            Dependencies = new List<JwellModuleInfo>();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is JwellModuleInfo))
            {
                return false;
            }

            JwellModuleInfo other = (JwellModuleInfo)obj;

            return Type == other.Type;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
