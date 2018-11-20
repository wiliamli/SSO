namespace Jwell.Framework.Excel
{
    
    internal class CellConfig
    {
       
        public string Title { get; set; }

       
        public bool AutoIndex { get; set; }

       
        public int Index { get; set; } = -1;

        
        public bool AllowMerge { get; set; }

        
        public bool IsIgnored { get; set; }

        public string Formatter { get; set; }
    }
}