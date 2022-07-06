using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
public class TVShowsController : BaseApiController {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public TVShowsController(IUnitOfWork unitOfWork, IMapper mapper) {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<PaginationResponse<TVShowToReturnDto>>> GetTVShows([FromQuery] TVShowSpecParams tvShowParams) {
        var spec = new TVShowsWithGenresSpecification(tvShowParams);
        var countSpec = new TVShowsWithFiltersForCountSpecification(tvShowParams);

        var totalItems = await _unitOfWork.Repository<TVShow>().CountAsync(countSpec);
        var tvShows = await _unitOfWork.Repository<TVShow>().GetListWithSpecAsync(spec);

        var data = _mapper.Map<IReadOnlyList<TVShow>, IReadOnlyList<TVShowToReturnDto>>(tvShows);
        var pageSize = tvShowParams.IsPaging ? tvShowParams.PageSize : totalItems;

        if (tvShows.Count() == 0) return Ok(new PaginationResponse<TVShowToReturnDto>(tvShowParams.PageIndex, pageSize, 0, data));

        return Ok(new PaginationResponse<TVShowToReturnDto>(tvShowParams.PageIndex, pageSize, totalItems, data));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TVShowToReturnDto>> GetTVShowById(int id) {
        var spec = new TVShowsWithGenresSpecification(id);
        var tvShow = await _unitOfWork.Repository<TVShow>().GetEntityWithSpec(spec);

        if (tvShow == null) return NotFound(new ApiResponse(404));

        return Ok(_mapper.Map<TVShow, TVShowToReturnDto>(tvShow));

    }

    [HttpPost]
    public async Task<ActionResult<TVShow>> CreateTVShow([FromBody] TVShow tvShow) {
        _unitOfWork.Repository<TVShow>().Add(tvShow);
        await _unitOfWork.CompleteAsync();
        return CreatedAtAction("GetTVShowById", new { id = tvShow.Id }, tvShow);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateTVShow(int id, [FromBody] TVShow tvShow) {
        if (id != tvShow.Id) return BadRequest(new ApiResponse(400));

        _unitOfWork.Repository<TVShow>().Update(tvShow);

        try {
            await _unitOfWork.CompleteAsync();
        } catch (DbUpdateConcurrencyException ex) {
            if (!await _unitOfWork.Repository<TVShow>().Contains(x => x.Id == id)) {
                return NotFound(new ApiResponse(404));
            } else {
                throw ex;
            }
        }

        return Ok($"TVShow id: {id} Updated!");
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteTVShow(int id) {
        var tvShow = await _unitOfWork.Repository<TVShow>().GetByIdAsync(id);

        if (tvShow == null) return NotFound(new ApiResponse(404));

        _unitOfWork.Repository<TVShow>().Remove(tvShow);

        await _unitOfWork.CompleteAsync();

        return Ok($"TVShow id: {id} successfully deleted.");
    }
}

