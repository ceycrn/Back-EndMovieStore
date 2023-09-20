using Core.DataAccess.EntityFramework;
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
    public class EfMovieDal : EfEntityRepositoryBase<Movie, NorthwindContext>, IMovieDal
    {
        public List<MovieDetailDto> GetMovieDetail()
        {
            // Product ve Categories tablolarını join ettik ve listesini döndük.
            using (NorthwindContext context = new NorthwindContext())
            {
                var result = from movie in context.Movies
                             join director in context.Directors
                             on movie.DirectorID equals director.DirectorID
                             select new MovieDetailDto
                             {
                                 MovieID = movie.MovieID,
                                 MovieName = movie.MovieName,
                                 DirectorName = director.Name
                             };
                return result.ToList();
            }
        }
    }
}
