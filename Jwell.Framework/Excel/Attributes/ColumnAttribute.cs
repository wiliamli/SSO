using System;

namespace Jwell.Framework.Excel
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ColumnAttribute : Attribute
    {
        /// <summary>
        /// <see cref="ColumnAttribute"/>
        /// </summary>
        public ColumnAttribute()
        {
            CellConfig = new CellConfig();
        }

        public string Title
        {
            get
            {
                return CellConfig.Title;
            }
            set
            {
                CellConfig.Title = value;
            }
        }

        public bool AutoIndex
        {
            get
            {
                return CellConfig.AutoIndex;
            }
            set
            {
                CellConfig.AutoIndex = value;
            }
        }

        public int Index
        {
            get
            {
                return CellConfig.Index;
            }
            set
            {
                CellConfig.Index = value;
            }
        }

        public bool AllowMerge
        {
            get
            {
                return CellConfig.AllowMerge;
            }
            set
            {
                CellConfig.AllowMerge = value;
            }
        }

        public bool IsIgnored
        {
            get
            {
                return CellConfig.IsIgnored;
            }
            set
            {
                CellConfig.IsIgnored = value;
            }
        }

		public string Formatter
        {
            get
            {
                return CellConfig.Formatter;
            }
            set
            {
                CellConfig.Formatter = value;
            }
        }

        internal CellConfig CellConfig { get; }
    }
}
