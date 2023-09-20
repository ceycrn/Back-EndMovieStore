using Business.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IBasketDetailService
    {
        IDataResult<List<BasketDetail>> GetAll();
        IResult AddBasket(addBasket basket);
        IResult DeleteBasket(int id);
        IDataResult<List<BasketDetail>> GetAllBasketDetail(int customerID);
        int GetCountOfMoviesInBasket(int customerID);
        decimal TotalPrice(int customerID);
    }
}
