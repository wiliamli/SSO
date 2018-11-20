using System;

namespace Jwell.Framework.Excel
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class StatisticsAttribute : Attribute
    {
        public StatisticsAttribute()
        {
            StatisticsConfig = new StatisticsConfig();
        }

        public string Name
        {
            get
            {
                return StatisticsConfig.Name;
            }
            set
            {
                StatisticsConfig.Name = value;
            }
        }

        public string Formula
        {
            get
            {
                return StatisticsConfig.Formula;
            }
            set
            {
                StatisticsConfig.Formula = value;
            }
        }

        public int[] Columns
        {
            get
            {
                return StatisticsConfig.Columns;
            }
            set
            {
                StatisticsConfig.Columns = value;
            }
        }

    
        internal StatisticsConfig StatisticsConfig { get; }
    }
}
