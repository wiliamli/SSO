using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Modules.Logger.Log.Model
{
    public class Marker
    {
        private const string DefaultModuleName = "default";

        public static readonly Marker Empty = new Marker();

        public string Module
        {
            get;
            private set;
        }

        public string Category
        {
            get;
            private set;
        }

        public string SubCategory
        {
            get;
            private set;
        }

        public Marker()
            : this("default")
        {
        }

        public Marker(string module)
            : this(module, "")
        {
        }

        public Marker(string module, string category)
            : this(module, category, "")
        {
        }

        public Marker(string module, string category, string subCategory)
        {
            Module = (string.IsNullOrEmpty(module) ? "default" : module);
            Category = (category ?? string.Empty);
            SubCategory = (subCategory ?? string.Empty);
        }
    }
}
