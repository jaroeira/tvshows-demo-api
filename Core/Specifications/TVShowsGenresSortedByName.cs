using Core.Entities;

namespace Core.Specifications;
public class TVShowsGenresSortedByName : BaseSpecification<TVShowGenre> {

    public TVShowsGenresSortedByName(string sort = null) {
        if (!string.IsNullOrEmpty(sort)) {
            switch (sort.ToLower()) {
                case "asc":
                    AddOrderBy(n => n.Name);
                    break;
                case "desc":
                    AddOrderByDescending(n => n.Name);
                    break;
                default:
                    AddOrderBy(n => n.Name);
                    break;
            }
        } else {
            AddOrderBy(n => n.Name);
        }
    }

}
