using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavouriteController : Controller
    {
        IFavouriteService _favouriteService;
        public FavouriteController(IFavouriteService favouriteService)
        {
            _favouriteService = favouriteService;
        }
        [HttpPost("GetAll")]
        public IActionResult GetAll()
        {
            var result = _favouriteService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("GetAllDetails")]
        public IActionResult GetDetails()
        {
            var result = _favouriteService.GetDetail();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("AddMovieAndCustomer")]
        public IActionResult Add(int customerID, int movieID)
        {
            var result = _favouriteService.Add(customerID, movieID);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("GetJustFavouriteMovies")]
        public IActionResult GetJustFavouriteMovies(int id)
        {
            var result = _favouriteService.GetJustFavouriteMovies(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);  
        }
    }
}
