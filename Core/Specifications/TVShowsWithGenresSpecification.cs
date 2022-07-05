using Core.Entities;

namespace Core.Specifications;

public class TVShowsWithGenresSpecification : BaseSpecification<TVShow>
{
    public TVShowsWithGenresSpecification(TVShowSpecParams tvShowParams) : base(x =>
      (string.IsNullOrEmpty(tvShowParams.Search)) || x.Name.ToLower().Contains(tvShowParams.Search) && // Filter by Name
      (!tvShowParams.GenreId.HasValue || x.TVShowGenreId == tvShowParams.GenreId) // Filter by Genre
    )
    {
        AddInclude(x => x.TVShowGenre); // Join

        // Pagination
        ApplyPaging(tvShowParams.PageSize * (tvShowParams.PageIndex - 1), tvShowParams.PageSize, tvShowParams.IsPaging);

        // Apply Sort
        if (!string.IsNullOrEmpty(tvShowParams.Sort))
        {
            switch (tvShowParams.Sort)
            {
                case "yearAsc":
                    AddOrderBy(y => y.Year);
                    break;
                case "yearDesc":
                    AddOrderByDescending(y => y.Year);
                    break;
                default:
                    AddOrderBy(n => n.Name);
                    break;
            }
        }
    }

    public TVShowsWithGenresSpecification(int id) : base(x => x.Id == id)
    {
        AddInclude(x => x.TVShowGenre);
    }
}
