namespace Invoice_Gen.ViewModels;

public class PagedResponse<T> where T : IPageable
{
    public List<T> Data { get; set; } = new();
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalRecords { get; set; }
}
