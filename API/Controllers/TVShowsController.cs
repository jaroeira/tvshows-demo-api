using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TVShowsController : BaseApiController
    {
        public TVShowsController()
        {
        }

        [HttpGet]
        public ActionResult GetTVShows()
        {
            return Ok("GetTVShows!!!!");
        }

        [HttpGet("{id}")]
        public ActionResult GetTVShowById(int id)
        {
            return Ok($"GetTVShow with id: {id}");
        }

        [HttpPost]
        public ActionResult CreateTVShow()
        {
            return Ok("Create TVShows!!!!");
        }

        [HttpPut("{id}")]
        public ActionResult UpdateTVShow(int id)
        {
            return Ok($"Update TVShows!!!! with id: {id}");
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTVShow(int id)
        {
            return Ok($"Delete TVShows!!!! with id: {id}");
        }
    }
}