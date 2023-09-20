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
    public class EfMovieAndActorDal : EfEntityRepositoryBase<MoviesAndActor, NorthwindContext>, IMovieAndActorDal
    {
        public List<MovieAndActorDetailDto> GetMovieAndActorDetail()
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var result = from x in context.ActorAndMovies
                             join actor in context.Actors
                             on x.ActorID equals actor.ActorID
                             join movie in context.Movies
                             on x.MovieID equals movie.MovieID
                             select new MovieAndActorDetailDto
                             {
                               ActorID = actor.ActorID, 
                               MovieID = movie.MovieID,     
                               MovieName = movie.MovieName, 
                               ActorName = actor.Name, 
                               ActorSurname = actor.Surname,  
                               Id=x.Id
                          
                             };
                return result.ToList();
            }
        }
    }
}
