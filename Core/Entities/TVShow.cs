namespace Core.Entities;

public class TVShow : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public decimal Rating { get; set; }
    public int Year { get; set; }
    public TVShowGenre TVShowGenre { get; set; }
    public int TVShowGenreId { get; set; }
}
