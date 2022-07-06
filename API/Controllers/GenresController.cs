using API.Errors;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        _unitOfWork.Repository<TVShowGenre>().Add(genre);
        await _unitOfWork.CompleteAsync();
        return CreatedAtAction("GetGenreById", new { id = genre.Id }, genre);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateGenre(int id, [FromBody] TVShowGenre genre) {
        if (id != genre.Id) return BadRequest(new ApiResponse(400));

        _unitOfWork.Repository<TVShowGenre>().Update(genre);

        try {
            await _unitOfWork.CompleteAsync();
        } catch (DbUpdateConcurrencyException ex) {
            if (!await _unitOfWork.Repository<TVShowGenre>().Contains(x => x.Id == id)) {
                return NotFound(new ApiResponse(404));
            } else {
                throw ex;
            }
        }

        return Ok($"TVShow Genre id: {id} Updated!");
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteGenre(int id) {
        var genre = await _unitOfWork.Repository<TVShowGenre>().GetByIdAsync(id);
        if (genre == null) return NotFound(new ApiResponse(404));
        _unitOfWork.Repository<TVShowGenre>().Remove(genre);
        await _unitOfWork.CompleteAsync();
        return Ok($"TVShow Genre id: {id} successfully deleted.");
    }
}
