using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TVShowsController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        public TVShowsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult GetTVShows()
        {
            return Ok("GetTVShows!!!!");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TVShow>> GetTVShowById(int id)
        {

            var tvShow = await _unitOfWork.Repository<TVShow>().GetByIdAsync(id);

            if (tvShow == null)
            {
                return NotFound();
            }

            return Ok(tvShow);


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