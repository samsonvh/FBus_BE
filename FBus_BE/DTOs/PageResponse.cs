namespace FBus_BE.DTOs
{
    public class PageResponse<T>
    {
        public List<T> Items { get; set; }
        public int PageIndex { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
    }
}
