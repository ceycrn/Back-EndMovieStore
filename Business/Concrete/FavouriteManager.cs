using Autofac.Core;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Castle.Core.Resource;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class FavouriteManager : IFavouriteService
    {
        IFavouriteDal _favouriteDal;
        IUserDal _userDal;
        IMovieDal _movieDal;

        public FavouriteManager(IFavouriteDal favouriteDal, IUserDal userDal, IMovieDal movieDal)
        {
            _favouriteDal = favouriteDal;
            _userDal = userDal;
            _movieDal = movieDal;
        }

        public IDataResult<List<Favourite>> GetAll()
        {
            var allFavourite = _favouriteDal.GetAll()
                .OrderBy(p => p.CustomerID)
                .ToList();
            return new SuccessDataResult<List<Favourite>>(allFavourite, Messages.ListedAllFavourites);
        }
        public IDataResult<List<FavouriteDetailDto>> GetDetail()
        {
            var allDetail = _favouriteDal.GetFavouriteDetail()
                 .OrderBy(p => p.CustomerName)
                 .ThenBy(p => p.CustomerSurname)
                 .ThenBy(p => p.MovieName)
                 .ToList();
            return new SuccessDataResult<List<FavouriteDetailDto>>(allDetail, Messages.ListedAllDetail);
        }

        [ValidationAspect(typeof(FavouriteValidator))]
        public IResult Add(int customerID, int movieID)
        {
            var favourite = new Favourite
            {
                CustomerID = customerID,
                MovieID = movieID,
            };

            IResult result = BusinessRules.Run(movieAndCustomerAreNull(favourite.CustomerID, favourite.MovieID));
            IResult test = CheckIfMovieAndCustomerExists(favourite.CustomerID, favourite.MovieID);
            if (!test.Success)
            {
                return test; // Zaten mevcutsa hata döndür
            }
            if (result != null)
            {
                return result;
            }

            _favouriteDal.Add(favourite);
            return new SuccessResult(Messages.givenMovieAndCustomerAdded);
        }

        [ValidationAspect(typeof(FavouriteValidator))]
        public IDataResult<List<JustMovies>> GetJustFavouriteMovies(int id)
        {
            var checkResult = CheckID(id);

            if (!checkResult.Success)
            {
                return new ErrorDataResult<List<JustMovies>>(Messages.UserNotFound);
            }

            var listedAll = _favouriteDal.GetFavouriteDetail()
                            .Where(p => p.CustomerID == id)
                            .ToList();

            var newList = listedAll.Select(x => new JustMovies
            {
                MovieName = x.MovieName,
            }).OrderBy(p => p.MovieName)
              .ToList();

            return new SuccessDataResult<List<JustMovies>>(newList, Messages.ListedJustFavouriteMovies);
        }


        private IResult movieAndCustomerAreNull(int customerID, int movieID)
        {
            var result = _userDal.GetAll(p => p.Id == customerID).Any() && _movieDal.GetAll(p => p.MovieID == movieID).Any();
            if (!result)
            {
                return new ErrorResult(Messages.CustomerMovieNotFound);
            }
            return new SuccessResult();
        }

        private IResult CheckIfMovieAndCustomerExists(int customerID, int movieID)
        {
            var existingRecord = _favouriteDal.GetAll().FirstOrDefault(x => x.MovieID == movieID && x.CustomerID == customerID);
            if (existingRecord != null)
            {
                return new ErrorResult(Messages.CustomerAndMovieAlreadyExists);
            }

            return new SuccessResult();
        }

        private IResult CheckIfCustomerExists(int customerID)
        {


            var existingRecord = _userDal.GetAll(x => x.Id == customerID);
            if (existingRecord != null)
            {
                return new SuccessResult();
            }

            return new ErrorResult(Messages.AlreadyExists);
        }

        private IResult CheckID(int id)
        {
            var user = _userDal.Get(p => p.Id == id);
            if (user == null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }
            return new SuccessResult();
        }

    }
}
