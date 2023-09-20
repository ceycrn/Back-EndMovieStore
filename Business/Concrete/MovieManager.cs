using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class MovieManager : IMovieService
    {
        IMovieDal _movieDal;
        public MovieManager(IMovieDal movieDal)
        {
            _movieDal = movieDal;

        }
        [ValidationAspect(typeof(MovieValidator))]
        public IResult Add(AddMovie movie)
        {
            var movieToAdd = new Movie
            {
                MovieName = movie.MovieName,
                MovieYear = movie.MovieYear,
                MovieGenre = movie.MovieGenre,
                Price = movie.Price,
                DirectorID = movie.DirectorID
            };

            IResult result = BusinessRules.Run(CheckIfMovieNameExists(movieToAdd.MovieName), CheckIfMovieYearValid(movieToAdd.MovieYear));
            if (result != null)
            {
                return result;
            }

            _movieDal.Add(movieToAdd);
            return new SuccessResult(Messages.MovieAdded);
        }

        public IResult Remove(string movieName)
        {
            var movieToDelete = _movieDal.Get(p => p.MovieName == movieName);

            if (movieToDelete != null)
            {
                _movieDal.Delete(movieToDelete);
                return new SuccessResult(Messages.MovieDeleted);
            }

            return new ErrorResult(Messages.MovieNotFound);
        }

        [ValidationAspect(typeof(MovieValidator))]
        public IResult Update(Movie movie)
        {
            var existingMovie = _movieDal.Get(m => m.MovieID == movie.MovieID);

            if (existingMovie != null)
            {
                existingMovie.MovieName = movie.MovieName;
                existingMovie.MovieYear = movie.MovieYear;
                existingMovie.MovieGenre = movie.MovieGenre;
                existingMovie.Price = movie.Price;
                existingMovie.DirectorID = movie.DirectorID;

                IResult result = BusinessRules.Run(CheckIfMovieYearValid(existingMovie.MovieYear));
                if (result != null)
                {
                    return result;
                }
                _movieDal.Update(existingMovie);
                return new SuccessResult(Messages.MovieUpdated);

            }

            return new ErrorResult(Messages.MovieNotFound);
        }

        public IDataResult<List<Movie>> getPriceBetween(int min, int max)
        {
            if (min == 0 && max == 0)
            {
                var ifIsNull = _movieDal.GetAll()
                             .OrderBy(p => p.Price)
                             .ThenBy(p => p.MovieName)
                             .ToList();
                return new SuccessDataResult<List<Movie>>(ifIsNull, Messages.ListedOfPriceBetweenGiven);
            }
            if (max == 0)
            {
                var ifIsNull = _movieDal.GetAll(p => p.Price >= min)
                                            .OrderBy(p => p.Price)
                                            .ThenBy(p => p.MovieName)
                                            .ToList();
                return new SuccessDataResult<List<Movie>>(ifIsNull, Messages.ListedOfPriceBetweenGiven);
            }
            var moviesInRange = _movieDal.GetAll(p => p.Price >= min && p.Price <= max)
                             .OrderBy(p => p.Price)
                             .ThenBy(p => p.MovieName)
                             .ToList();
            return new SuccessDataResult<List<Movie>>(moviesInRange, Messages.ListedOfPriceBetweenGiven);
        }

        public IDataResult<List<Movie>> OrderPriceByDescending()
        {
            var descendingOrder = _movieDal.GetAll()
                                           .OrderByDescending(p => p.Price)
                                           .ThenBy(p => p.MovieName)
                                           .ToList();
            return new SuccessDataResult<List<Movie>>(descendingOrder, Messages.descendingOrder);
        }

        public IDataResult<List<Movie>> OrderPriceByAscending()
        {
            var descendingOrder = _movieDal.GetAll()
                                            .OrderBy(p => p.Price)
                                            .ThenBy(p => p.MovieName)
                                            .ToList();
            return new SuccessDataResult<List<Movie>>(descendingOrder, Messages.ascendingOrder);
        }

        public IDataResult<List<Movie>> Search(string movieName)
        {
            var searchResults = _movieDal.GetAll(p => p.MovieName.Contains(movieName))
                                         .OrderBy(p => p.MovieName)
                                         .ToList();

            return new SuccessDataResult<List<Movie>>(searchResults,Messages.searchingMoviesList);
        }

        public IDataResult<List<Movie>> GetAll()
        {
            return new SuccessDataResult<List<Movie>>(_movieDal.GetAll(),
                Messages.ListedAllMovies);
        }

        [ValidationAspect(typeof(MovieValidator))]
        private IResult CheckIfMovieNameExists(string movieName)
        {
            var result = _movieDal.GetAll(p => p.MovieName == movieName).Any();
            if (result)
            {
                return new ErrorResult(Messages.MovieNameAlreadyExists);
            }
            return new SuccessResult();
        }

        public IResult CheckIfMovieYearValid(int movieYear)
        {
            bool result = movieYear <= DateTime.Now.Year;
            if (result)
            {
                return new SuccessResult(Messages.MovieYearIsValid);

            }

            return new ErrorResult(Messages.MovieYearIsInvalid);

        }


    }
}
