using System;

namespace Jwell.Framework.Excel
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class FreezeAttribute : Attribute
    {
       
        public FreezeAttribute()
        {
            FreezeConfig = new FreezeConfig();
        }

      
        public int ColSplit
        {
            get
            {
                return FreezeConfig.ColSplit;
            }
            set
            {
                FreezeConfig.ColSplit = value;
            }
        }

     
        public int RowSplit
        {
            get
            {
                return FreezeConfig.RowSplit;
            }
            set
            {
                FreezeConfig.RowSplit = value;
            }
        }

     
        public int LeftMostColumn
        {
            get
            {
                return FreezeConfig.LeftMostColumn;
            }
            set
            {
                FreezeConfig.LeftMostColumn = value;
            }
        }

       
        public int TopRow
        {
            get
            {
                return FreezeConfig.TopRow;
            }
            set
            {
                FreezeConfig.TopRow = value;
            }
        }

        internal FreezeConfig FreezeConfig { get; }
    }
}
