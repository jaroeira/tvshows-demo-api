namespace Core.Specifications;

public class TVShowSpecParams
{
    private const int MaxPageSize = 50;
    public int PageIndex { get; set; } = 1;
    private int _pageSize = 6;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }

    public int? GenreId { get; set; }
    public string Sort { get; set; } = "Name";

    public bool IsPaging { get; set; } = true;

    private string _search;
    public string Search
    {
        get => _search;
        set => _search = value.ToLower();
    }
}
