using API.Errors;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class GenresController : BaseApiController {

    private readonly IUnitOfWork _unitOfWork;
    public GenresController(IUnitOfWork unitOfWork) {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TVShowGenre>>> GetGenres([FromQuery] string sort) {
        var spec = new TVShowsGenresSortedByName(sort);
        var genres = await _unitOfWork.Repository<TVShowGenre>().GetListWithSpecAsync(spec);
        return Ok(genres);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TVShowGenre>> GetGenreById(int id) {
        var genre = await _unitOfWork.Repository<TVShowGenre>().GetByIdAsync(id);
        if (genre == null) return NotFound(new ApiResponse(404));
        return Ok(genre);
    }

    [HttpPost]
    public async Task<ActionResult<TVShowGenre>> AddGenre([FromBody] TVShowGenre genre) {
        System.Console.WriteLine($"Genre - id: {genre.Id} name: {genre.Name}");

        _unitOfWork.Repository<TVShowGenre>().Add(genre);
        await _unitOfWork.CompleteAsync();
        return CreatedAtAction("GetGenreById", new { id = genre.Id }, genre);
    }


    [HttpPut("{id}")]
    public ActionResult UpdateGenre(int id) {
        return Ok($"Update Genre with id: {id}");
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteGenre(int id) {
        return Ok($"Delete Genre!!!! with id: {id}");
    }
}
