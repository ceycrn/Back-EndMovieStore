using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BasketDetailManager : IBasketDetailService
    {
        IBasketDetailDal _basketDetailDal;
        IUserDal _userDal;
        IMovieDal _movieDal;

        public BasketDetailManager(IBasketDetailDal basketDetailDal, IUserDal userDal, IMovieDal movieDal)
        {
            _basketDetailDal = basketDetailDal;
            _userDal = userDal;
            _movieDal = movieDal;
        }

        public IDataResult<List<BasketDetail>> GetAll()
        {
            var listedBasketDetail = _basketDetailDal.GetAll()
                .OrderBy(p => p.CustomerID)
                .ThenBy(p => p.CustomerFullName)
                .ThenBy(p => p.MovieID)
                .ThenBy(p => p.MovieName)
                .ThenBy(p => p.MoviePrice)
                .ToList();

            return new SuccessDataResult<List<BasketDetail>>(listedBasketDetail, Messages.ListedAllBasketDetail);
        }
        [ValidationAspect(typeof(BasketDetailValidator))]
        public IResult AddBasket(addBasket basket)
        {
            var user = _userDal.Get(p => p.Id == basket.customerID);
            var movie = _movieDal.Get(p => p.MovieID == basket.movieID);
            var newList = new BasketDetail
            {
                CustomerID = user.Id,
                MovieID = movie.MovieID,
                MovieName = movie.MovieName,
                MoviePrice = movie.Price,
                DateOfAdding = DateTime.Now,
                CustomerFullName = user.FirstName + " " + user.LastName,
            };
            IResult result = BusinessRules.Run(CheckNull(basket.customerID, basket.movieID));
            if (result != null)
            {
                return result;
            }
            try
            {
                _basketDetailDal.Add(newList);
                return new SuccessResult(Messages.givenMovieAndCustomerAdded);
            }
            catch (DbUpdateException ex)
            {
                return new ErrorResult(ex.InnerException?.Message);
            }
        }
        public IResult DeleteBasket(int id)
        {

            var BasketToDelete = _basketDetailDal.Get(p => p.Id == id);

            if (BasketToDelete != null)
            {
                _basketDetailDal.Delete(BasketToDelete);
                return new SuccessResult(Messages.MovieinBasketDeleted);
            }

            return new ErrorResult(Messages.BasketIDNotFound);
        }
        [ValidationAspect(typeof(BasketDetailValidator))]
        public IDataResult<List<BasketDetail>> GetAllBasketDetail(int customerID)
        {
            var list = new List<BasketDetail>();
            IResult result = BusinessRules.Run(CheckID(customerID));
            if (result != null)
            {
                return new ErrorDataResult<List<BasketDetail>>(list, Messages.ActorMovieNotFound);
            }
            var ListOfAll = _basketDetailDal.GetAll(p => p.CustomerID == customerID);
            return new SuccessDataResult<List<BasketDetail>>(ListOfAll, Messages.ListedPersonelBasket);
        }

        public int GetCountOfMoviesInBasket(int customerID)
        {
            IResult result = CheckID(customerID);
            if (!result.Success)
            {
                return 0;
            }
            var count = _basketDetailDal.GetAll(p => p.CustomerID == customerID).Count;
            return count;
        }

        public decimal TotalPrice(int customerID)
        {
            IResult result = CheckID(customerID);
            if (!result.Success)
            {
                return 0;
            }
            var totalPrice = _basketDetailDal.GetAll(p => p.CustomerID == customerID);
            decimal total = 0;
            foreach (var value in totalPrice)
            {
                total += value.MoviePrice;
            }
            return total;
        }

        private IResult CheckNull(int cID, int mID)
        {
            var result = _userDal.GetAll(p => p.Id == cID).Any() && _movieDal.GetAll(x => x.MovieID == mID).Any();
            if (!result)
            {
                return new ErrorResult(Messages.CustomerMovieNotFound);
            }
            return new SuccessResult();
        }
        private IResult CheckID(int cID)
        {
            var result = _userDal.GetAll(p => p.Id == cID).Any();
            if (!result)
            {
                return new ErrorResult(Messages.CustomerMovieNotFound);
            }
            return new SuccessResult();
        }

    }
}