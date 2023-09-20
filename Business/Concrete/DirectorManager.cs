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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class DirectorManager : IDirectorService
    {
        IDirectorDal _directorDal;
        public DirectorManager(IDirectorDal directorDal)
        {
            _directorDal = directorDal;

        }

        public IDataResult<List<Director>> GetAll()
        {
            return new SuccessDataResult<List<Director>>(_directorDal.GetAll(),
                Messages.ListedAllDirectors);
        }
        public IDataResult<List<DirectorDetailDto>> GetAllMovies(int id)
        {
            var AllMovies = _directorDal.GetDirectorDetail()
                .Where(p => p.DirectorId == id)
                .OrderBy(p => p.Name)
                .ThenBy(p => p.MovieName)
                .ToList();
            return new SuccessDataResult<List<DirectorDetailDto>>(AllMovies, Messages.DirectorAndMoviesListed);
        }

        [ValidationAspect(typeof(DirectorValidator))]
        public IResult Add(AddDirector director)
        {
            var directorToAdd = new Director
            {
                Name = director.Name,
                Surname = director.Surname,
            };

            IResult result = BusinessRules.Run(CheckIfDirectorNameExists(directorToAdd.Name, directorToAdd.Surname));
            if (result != null)
            {
                return result;
            }

            _directorDal.Add(directorToAdd);
            return new SuccessResult(Messages.DirectorAdded);
        }
        public IResult Delete(int id)
        {
            var directorToDelete = _directorDal.Get(p => p.DirectorID == id);

            if (directorToDelete != null)
            {
                _directorDal.Delete(directorToDelete);
                return new SuccessResult(Messages.DirectorDeleted);
            }

            return new ErrorResult(Messages.DirectorNotFound);
        }

        #region İŞ KURALLARI
        [ValidationAspect(typeof(DirectorValidator))]
        private IResult CheckIfDirectorNameExists(string directorName, string surname)
        {
            var result = _directorDal.GetAll(p => p.Name == directorName).Any() && _directorDal.GetAll(p => p.Surname == surname).Any();
            if (result)
            {
                return new ErrorResult(Messages.DirectorAlreadyAdded);
            }
            return new SuccessResult();
        }
        #endregion

    }

}
