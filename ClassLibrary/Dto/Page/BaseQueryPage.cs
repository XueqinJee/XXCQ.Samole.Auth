namespace ClassLibrary.Dto.Page
{
    public class BaseQueryPage
    {
        public string? q { get; set; } = "";
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
