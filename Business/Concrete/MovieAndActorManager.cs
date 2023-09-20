using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Castle.DynamicProxy.Generators;
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
    public class MovieAndActorManager : IMovieAndActorService
    {
        IMovieAndActorDal _movieAndActorDal;
        IActorDal _actorDal;
        IMovieDal _movieDal;
        public MovieAndActorManager(IMovieAndActorDal movieAndActorDal, IActorDal actorDal, IMovieDal movieDal)
        {
            _movieAndActorDal = movieAndActorDal;
            _actorDal = actorDal;
            _movieDal = movieDal;
        }

        public IDataResult<List<MoviesAndActor>> GetAll()
        {
            var listedMovieAndActor = _movieAndActorDal.GetAll()
                .OrderBy(p => p.ActorID)
                .ThenBy(p => p.MovieID)
                .ToList();

            return new SuccessDataResult<List<MoviesAndActor>>(listedMovieAndActor, Messages.ListedAllMovieAndActor);

        }

        public IDataResult<List<MovieAndActorDetailDto>> GetDetail()
        {
            var listedAll = _movieAndActorDal.GetMovieAndActorDetail()
                .OrderBy(p => p.MovieName)
                .ThenBy(p => p.ActorName)
                .ToList();
            return new SuccessDataResult<List<MovieAndActorDetailDto>>(listedAll, Messages.ListedDetails);
        }
        public IDataResult<List<JustActors>> GetJustActors(int id)
        {
            if (CheckIDisNullorExists(id).Success)
            {
                var listedAll = _movieAndActorDal.GetMovieAndActorDetail()
               .Where(p => p.MovieID == id)
               .OrderBy(p => p.ActorName)
               .ToList();

                var newList = listedAll.Select(x => new JustActors
                {
                    FullName = x.ActorName + " " + x.ActorSurname
                }).OrderBy(p => p.FullName)
                  .ToList();

                return new SuccessDataResult<List<JustActors>>(newList, Messages.ListedJustActors);
            }
            else
                return new ErrorDataResult<List<JustActors>>(Messages.MovieIDNotFound);

        }
        [ValidationAspect(typeof(MovieAndActor))]
        public IResult Add(int actorID, int movieID)
        {
            var actorAndMoviesAdd = new MoviesAndActor
            {
                ActorID = actorID,
                MovieID = movieID,
            };

            IResult result = BusinessRules.Run(movieAndActorAreNull(actorAndMoviesAdd.ActorID, actorAndMoviesAdd.MovieID));
            IResult test = CheckIfMovieAndActorExists(actorAndMoviesAdd.ActorID, actorAndMoviesAdd.MovieID);
            if (!test.Success)
            {
                return test; // Zaten mevcutsa hata döndür
            }
            if (result != null)
            {
                return result;
            }

            _movieAndActorDal.Add(actorAndMoviesAdd);
            return new SuccessResult(Messages.givenMovieAndActorAdded);
        }

        public IDataResult<List<JustMovies>> GetJustMovies(int id)
        {
            if (CheckMovieIDisNullorExists(id).Success)
            {
                var listedAll = _movieAndActorDal.GetMovieAndActorDetail()
               .Where(p => p.ActorID == id)
               .ToList();

                var newList = listedAll.Select(x => new JustMovies
                {
                    MovieName = x.MovieName,
                }).OrderBy(p => p.MovieName)
                  .ToList();

                return new SuccessDataResult<List<JustMovies>>(newList, Messages.ListedJustMovies);
            }
            else
                return new ErrorDataResult<List<JustMovies>>(Messages.ActorNotFound);

        }

        #region İŞ KURALLARI
        private IResult CheckIDisNullorExists(int id)
        {
            var result = _movieAndActorDal.GetAll(p => p.MovieID == id).Any();
            if (!result)
            {
                return new ErrorResult(Messages.MovieIDNotFound);
            }
            return new SuccessResult();
        }
        private IResult CheckMovieIDisNullorExists(int id)
        {
            var result = _movieAndActorDal.GetAll(p => p.ActorID == id).Any();
            if (!result)
            {
                return new ErrorResult(Messages.ActorNotFound);
            }
            return new SuccessResult();
        }
        private IResult movieAndActorAreNull(int actorID, int movieID)
        {
            var result = _actorDal.GetAll(p => p.ActorID == actorID).Any() && _movieDal.GetAll(p => p.MovieID == movieID).Any();
            if (!result)
            {
                return new ErrorResult(Messages.ActorMovieNotFound);
            }
            return new SuccessResult();
        }
        private IResult CheckIfMovieAndActorExists(int actorID, int movieID)
        {
            var existingRecord = _movieAndActorDal.GetAll().FirstOrDefault(x => x.MovieID == movieID && x.ActorID == actorID);
            if (existingRecord != null)
            {
                return new ErrorResult(Messages.AlreadyExists);
            }

            return new SuccessResult();
        }

        #endregion
    }
}

