using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController : Controller
    {
        IDirectorService _directorService;
        public DirectorController(IDirectorService directorService)
        {
            _directorService = directorService;
        }

        [HttpPost("GetAll")]
        public IActionResult GetAll()
        {
            var result = _directorService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("addDirector")]
        public IActionResult Add(AddDirector director)
        {
            var result = _directorService.Add(director);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("deletedirector")]
        public IActionResult Delete(int id)
        {
            var result = _directorService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("GetAllMovies")]
        public IActionResult GetAllMovies(int id)
        {
            var result = _directorService.GetAllMovies(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
