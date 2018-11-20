using System;

namespace Jwell.Framework.Excel
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class FilterAttribute : Attribute
    {
        /// <summary>
        /// <see cref="FilterAttribute"/>
        /// </summary>
        public FilterAttribute()
        {
            FilterConfig = new FilterConfig();
        }

        public int FirstRow
        {
            get
            {
                return FilterConfig.FirstRow;
            }
            set
            {
                FilterConfig.FirstRow = value;
            }
        }

        public int? LastRow
        {
            get
            {
                return FilterConfig.LastRow;
            }
            set
            {
                FilterConfig.LastRow = value;
            }
        }

        public int FirstCol
        {
            get
            {
                return FilterConfig.FirstCol;
            }
            set
            {
                FilterConfig.FirstCol = value;
            }
        }

        public int LastCol
        {
            get
            {
                return FilterConfig.LastCol;
            }
            set
            {
                FilterConfig.LastCol = value;
            }
        }

        internal FilterConfig FilterConfig { get; }
    }
}
