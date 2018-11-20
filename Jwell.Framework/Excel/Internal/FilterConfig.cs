namespace Jwell.Framework.Excel
{ 
    internal class FilterConfig
    {
        
        public int FirstRow { get; set; }

       
        public int? LastRow { get; set; } = null;

       
        public int FirstCol { get; set; }

        public int LastCol { get; set; }
    }
}