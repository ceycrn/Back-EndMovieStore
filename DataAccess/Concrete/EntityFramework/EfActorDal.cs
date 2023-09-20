using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfActorDal : EfEntityRepositoryBase<Actor, NorthwindContext>, IActorDal
    {
        public List<ActorDetailDto> GetActorDetail()
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var result = from actor in context.Actors
                             select new ActorDetailDto
                             {
                                 ActorID = actor.ActorID,
                                 Name = actor.Name,
                                 Surname = actor.Surname,
                                 Age = actor.Age,
                                 Gender = actor.Gender,
                             };
                return result.ToList();
            }
        }
    }
}
