namespace Jwell.Framework.Paging
{
    public class PageParam
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public string Sort { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}
