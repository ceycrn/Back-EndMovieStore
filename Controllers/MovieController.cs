using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        IMovieService _movieService;
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpPost("getall")]
        public IActionResult GetAll()
        {
            var result = _movieService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("addmovie")]
        public IActionResult Add(AddMovie movie)
        {
            var result = _movieService.Add(movie);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("deletemovie")]
        public IActionResult Remove(string movieName)
        {
            var result = _movieService.Remove(movieName);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("updatemovie")]
        public IActionResult Update(Movie movie)
        {
            if (movie == null)
            {
                return BadRequest("Movie object is null");
            }

            var result = _movieService.Update(movie);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("betweenPrice")]
        public IActionResult BetweenPrices(int min, int max)
        {

            var result = _movieService.getPriceBetween(min, max);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("descendingOrder")]
        public IActionResult DescendingOrder()
        {
            var result = _movieService.OrderPriceByDescending();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("ascendingOrder")]
        public IActionResult AscendingOrder()
        {
            var result = _movieService.OrderPriceByAscending();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("searching")]
        public IActionResult Searching(string MovieName)
        {
            var result = _movieService.Search(MovieName);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
