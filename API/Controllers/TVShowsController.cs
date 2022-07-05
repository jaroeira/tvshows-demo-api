using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TVShowsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TVShowsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<TVShow>>> GetTVShows([FromQuery] TVShowSpecParams tvShowParams)
        {
            var spec = new TVShowsWithGenresSpecification(tvShowParams);
            var tvShows = await _unitOfWork.Repository<TVShow>().GetListWithSpecAsync(spec);
            return Ok(_mapper.Map<IReadOnlyList<TVShow>, IReadOnlyList<TVShowToReturnDto>>(tvShows));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TVShow>> GetTVShowById(int id)
        {
            var spec = new TVShowsWithGenresSpecification(id);
            var tvShow = await _unitOfWork.Repository<TVShow>().GetEntityWithSpec(spec);

            if (tvShow == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TVShow, TVShowToReturnDto>(tvShow));

        }

        [HttpPost]
        public ActionResult CreateTVShow()
        {
            return Ok("Create TVShows!!!!");
        }

        [HttpPut("{id}")]
        public ActionResult UpdateTVShow(int id)
        {
            return Ok($"Update TVShow with id: {id}");
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTVShow(int id)
        {
            return Ok($"Delete TVShows!!!! with id: {id}");
        }
    }
}