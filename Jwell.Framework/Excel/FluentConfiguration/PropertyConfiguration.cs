using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwell.Framework.Excel
{
    public class PropertyConfiguration
    {
       
        public PropertyConfiguration()
        {
            CellConfig = new CellConfig();
        }

     
        internal CellConfig CellConfig { get; }

      
        public PropertyConfiguration HasExcelIndex(int index)
        {
            CellConfig.Index = index;

            return this;
        }

    
        public PropertyConfiguration HasExcelTitle(string title)
        {
            CellConfig.Title = title;

            return this;
        }

      
        public PropertyConfiguration HasDataFormatter(string formatter)
        {
            CellConfig.Formatter = formatter;

            return this;
        }

       
        public PropertyConfiguration HasAutoIndex()
        {
            CellConfig.AutoIndex = true;

            return this;
        }

       
        public PropertyConfiguration IsMergeEnabled()
        {
            CellConfig.AllowMerge = true;

            return this;
        }

      
        public void IsIgnored()
        {
            CellConfig.IsIgnored = true;
        }

        
        public void HasExcelCell(int index, string title, string formatter, bool allowMerge)
        {
            CellConfig.Index = index;
            CellConfig.Title = title;
            CellConfig.Formatter = formatter;
            CellConfig.AutoIndex = false;
            CellConfig.AllowMerge = allowMerge;
        }

        
        public void HasExcelCell(string title, string formatter, bool allowMerge)
        {
            CellConfig.Index = -1;
            CellConfig.Title = title;
            CellConfig.Formatter = formatter;
            CellConfig.AutoIndex = true;
            CellConfig.AllowMerge = allowMerge;
        }
    }
}
