using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ActorController: ControllerBase
    {
        IActorService _actorService;
        public ActorController(IActorService actorService)
        {
            _actorService = actorService;
        }

        
        [HttpPost("addactor")]
        public IActionResult Add(AddActor actor)
        {
            var result = _actorService.Add(actor);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("deleteactor")]
        public IActionResult Delete(int id)
        {
            var result=_actorService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);  
        }


        [HttpPost("getall")]
        public IActionResult GetAll()
        {
            var result = _actorService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
