namespace API.Helpers;

public class PaginationResponse<T> where T : class {
    public PaginationResponse(int pageIndex, int pageSize, int count, IReadOnlyList<T> data) {
        PageIndex = pageIndex;
        PageSize = pageSize;
        Count = count;
        Data = data;
    }

    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalPages {
        get {
            return (int)Math.Ceiling((decimal)Count / (decimal)PageSize);
        }
    }
    public int Count { get; set; }

    public IReadOnlyList<T> Data { get; set; }
}
