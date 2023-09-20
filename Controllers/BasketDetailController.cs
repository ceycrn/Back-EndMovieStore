using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketDetailController : Controller
    {
        IBasketDetailService _basketDetailService;

        public BasketDetailController(IBasketDetailService basketDetailService)
        {
            _basketDetailService = basketDetailService;
        }

        [HttpPost("GetAll")]
        public IActionResult GetAll()
        {
            var result = _basketDetailService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("Add")]
        public IActionResult Add(addBasket basket)
        {
            var result = _basketDetailService.AddBasket(basket);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("Delete")]
        public IActionResult Delete(int id)
        {
            var result = _basketDetailService.DeleteBasket(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("GetPersonelBasket")]
        public IActionResult getPersonelBasket(int id)
        {
            var result = _basketDetailService.GetAllBasketDetail(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("GetCountOfMoviesInBasket")]
        public int getCountOfBasket(int id)
        {
            var result = _basketDetailService.GetCountOfMoviesInBasket(id);
            if (result != 0)
            {
                return result;
            }
            return result;
        }  

        [HttpPost("GetTotalPrice")]
        public decimal getTotal(int id)
        {
            decimal result = _basketDetailService.TotalPrice(id);
            if (result != 0)
            {
                return result;
            }
            return result;
        }

    }
}
