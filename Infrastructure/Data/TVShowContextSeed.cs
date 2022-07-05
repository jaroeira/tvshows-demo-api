using System.Text.Json;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data;

public class TVShowContextSeed
{
    public static async Task SeedAsync(TVShowContext context, ILoggerFactory loggerFactory)
    {
        try
        {

            //Seed TVShowGenres
            if (!context.TVShowGenres!.Any())
            {
                var genresData = File.ReadAllText("../Infrastructure/Data/SeedData/genres.json");
                var genres = JsonSerializer.Deserialize<List<TVShowGenre>>(genresData);

                foreach (var genre in genres!)
                {
                    context.TVShowGenres!.Add(genre);
                }

                await context.SaveChangesAsync();
            }

            //Seed TVShows
            if (!context.TVShows!.Any())
            {
                var tvShowsData = File.ReadAllText("../Infrastructure/Data/SeedData/tv-shows.json");
                var tvShows = JsonSerializer.Deserialize<List<TVShow>>(tvShowsData);

                foreach (var tvShow in tvShows!)
                {
                    context.TVShows!.Add(tvShow);
                }

                await context.SaveChangesAsync();
            }




        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<TVShowContextSeed>();
            logger.LogError(ex.Message);
        }
    }
}
