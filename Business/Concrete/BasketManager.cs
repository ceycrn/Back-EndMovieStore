using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BasketManager : IBasketService
    {
        IBasketDal _basketDal;
        IUserDal _userDal;
        IMovieDal _movieDal;
        IBasketDetailDal _basketDetailDal;
        public BasketManager(IBasketDal basketDal, IUserDal userDal, IMovieDal movieDal, IBasketDetailDal basketDetailDal)
        {
            _basketDal = basketDal;
            _userDal = userDal;
            _movieDal = movieDal;
            _basketDetailDal = basketDetailDal;
        }
        [ValidationAspect(typeof(BasketValidator))]
        public IResult AddBasket(int customerID)
        {
            IResult result = BusinessRules.Run(isExists(customerID));
            if (result != null)
            {
                return new ErrorResult(Messages.CustomerAlreadyExists);
            }
            IResult test = BusinessRules.Run(checkIsExists(customerID));
            if (test != null)
            {
                return new ErrorResult(Messages.CustomerAlreadyExists);
            }

            decimal total = TotalPrice(customerID);

            var newBasket = new Basket
            {
                CustomerID = customerID,
                TotalPrice = total,
            };

            _basketDal.Add(newBasket);
            return new SuccessResult(Messages.BasketAdded);
        }
        public IResult Buy(int customerID)
        {

            var users = _userDal.Get(p => p.Id == customerID);
            decimal budget = users.Budget;
            decimal total = TotalPrice(customerID);

            //total price eger 350'den fazlaysa %15 indirim uygulasın ve ve basketdal'da total price alanını güncellesin
            if (total > 350)
            {
                decimal discount = total * 0.15m;
                total -= discount;
                // Sepetin total price alanını güncelle
                var basket = _basketDal.Get(p => p.CustomerID == customerID);
                if (basket != null)
                {
                    basket.TotalPrice = total;
                    _basketDal.Update(basket);
                }
            }
            if (budget > total)
            {
                //users budget - total yap ve update et
                var user = _userDal.Get(p => p.Id == customerID);
                user.Budget -= total;
                _userDal.Update(user);

                //basket o kişiyi sil
                var basket = _basketDal.Get(p => p.CustomerID == customerID);
                if (basket != null)
                {
                    _basketDal.Delete(basket);
                    //basketDetail o kişiye ait satırları dal'dan sil
                    var basketDetails = _basketDetailDal.GetAll(p => p.CustomerID == customerID);
                    foreach (var basketDetail in basketDetails)
                    {
                        _basketDetailDal.Delete(basketDetail);
                    }
                }
                return new SuccessResult(Messages.BuyIsSuccess);
            }
            return new ErrorResult(Messages.BudgetLimitExceed);
        }
        public IDataResult<List<Basket>> GetAll()
        {
            var listedBasket = _basketDal.GetAll()
                .OrderBy(p => p.CustomerID)
                .ToList();

            return new SuccessDataResult<List<Basket>>(listedBasket, Messages.ListedAllBasket);
        }
        #region BusinessRules
        private IResult isExists(int id)
        {
            var result = _userDal.GetAll(p => p.Id == id).Any();
            if (result == false)
            {
                return new ErrorResult(Messages.CustomerMovieNotFound);
            }
            return new SuccessResult();
        }
        private IResult checkIsExists(int id)
        {
            var result = _basketDal.GetAll(p => p.Id == id).Any();
            if (result == true)
            {
                return new ErrorResult(Messages.CustomerMovieNotFound);
            }
            return new SuccessResult();
        }
        private decimal TotalPrice(int customerID)
        {
            var totalPrice = _basketDetailDal.GetAll(p => p.CustomerID == customerID);
            decimal total = 0;
            foreach (var value in totalPrice)
            {
                total += value.MoviePrice;
            }
            return total;
        }
        #endregion
    }
}
