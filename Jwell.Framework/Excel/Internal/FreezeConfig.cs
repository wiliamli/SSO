namespace Jwell.Framework.Excel
{
    internal class FreezeConfig
    {
       
        public int ColSplit { get; set; } = 0;

       
        public int RowSplit { get; set; } = 1;

      
        public int LeftMostColumn { get; set; } = 0;

        public int TopRow { get; set; } = 1;
    }
}