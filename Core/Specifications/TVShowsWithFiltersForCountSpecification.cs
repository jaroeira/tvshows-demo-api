using Core.Entities;

namespace Core.Specifications;

public class TVShowsWithFiltersForCountSpecification : BaseSpecification<TVShow> {
    public TVShowsWithFiltersForCountSpecification(TVShowSpecParams tvShowParams) : base(x =>
      (string.IsNullOrEmpty(tvShowParams.Search)) || x.Name.ToLower().Contains(tvShowParams.Search) && // Filter by Name
      (!tvShowParams.GenreId.HasValue || x.TVShowGenreId == tvShowParams.GenreId) // Filter by Genre
    ) {
    }
}
