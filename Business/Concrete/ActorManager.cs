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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ActorManager : IActorService
    {
        IActorDal _actorDal;
        public ActorManager(IActorDal actorDal)
        {
            _actorDal = actorDal;
        }

        [ValidationAspect(typeof(ActorValidator))]
        public IResult Add(AddActor actor)
        {
            var actorToAdd = new Actor
            {
                Name = actor.Name,
                Surname = actor.Surname,
                Age = actor.Age,
                Gender = actor.Gender,
            };

            IResult result = BusinessRules.Run(CheckIfActorNameExists(actorToAdd.Name, actorToAdd.Surname, actorToAdd.Gender));
            if (result != null)
            {
                return result;
            }

            _actorDal.Add(actorToAdd);
            return new SuccessResult(Messages.ActorAdded);
        }

        public IResult Delete(int id)
        {

            var actorToDelete = _actorDal.Get(p => p.ActorID == id);

            if (actorToDelete != null)
            {
                _actorDal.Delete(actorToDelete);
                return new SuccessResult(Messages.ActorDeleted);
            }

            return new ErrorResult(Messages.ActorNotFound);
        }

        public IDataResult<List<Actor>> GetAll()
        {
            var listedActor = _actorDal.GetAll()
                .OrderBy(p => p.Name)
                .ThenBy(p => p.Surname)
                .ToList();

            return new SuccessDataResult<List<Actor>>(listedActor, Messages.ListedAllActors);
        }

        #region Is Kurallari
        [ValidationAspect(typeof(ActorValidator))]
        private IResult CheckIfActorNameExists(string actorName, string surname, bool gender)
        {
            // Belirtilen cinsiyette en az bir aktörün varlığını kontrol edin
            var genderExists = _actorDal.GetAll(p => p.Gender == gender).Any();

            if (genderExists)
            {
                // İsim ve soyisimdeki aktörleri kontrol edin
                var actorExists = _actorDal.GetAll(p => p.Name == actorName && p.Surname == surname && p.Gender == gender).Any();

                if (actorExists)
                {
                    // İsim ve soyisimdeki aktör zaten varsa hata döndürün
                    return new ErrorResult(Messages.ActorAlreadyAdded);
                }
                else
                {
                    // İsim ve soyisimdeki aktör yoksa başarılı sonuç döndürün
                    return new SuccessResult();
                }
            }
            else
            {
                // Belirtilen cinsiyette hiç aktör yoksa yine başarılı sonuç döndürün
                return new SuccessResult();
            }
        }


        #endregion
    }
}
