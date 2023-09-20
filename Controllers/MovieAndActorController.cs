using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieAndActorController : Controller
    {

        IMovieAndActorService _IMovieAndActorService;
        public MovieAndActorController(IMovieAndActorService movieAndActorService)
        {
            _IMovieAndActorService = movieAndActorService;
        }

        [HttpPost("GetAll")]
        public IActionResult GetAll()
        {
            var result = _IMovieAndActorService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("GetAllDetails")]
        public IActionResult GetDetails()
        {
            var result = _IMovieAndActorService.GetDetail();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("GetJustActors")]
        public IActionResult GetJustActors(int movieID)
        {
            var result = _IMovieAndActorService.GetJustActors(movieID);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("GetJustMovies")]
        public IActionResult GetJustMovies(int actorID)
        {
            var result = _IMovieAndActorService.GetJustMovies(actorID);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("AddMovieAndActor")]
        public IActionResult Add(AddMoviesAndActor addMoviesAndActor)
        {
            var result = _IMovieAndActorService.Add(addMoviesAndActor.ActorID, addMoviesAndActor.MovieID);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
