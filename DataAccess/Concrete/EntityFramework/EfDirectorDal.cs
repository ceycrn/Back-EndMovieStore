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
    public class EfDirectorDal : EfEntityRepositoryBase<Director, NorthwindContext>, IDirectorDal
    {
        public List<DirectorDetailDto> GetDirectorDetail()
        {
            // Product ve Categories tablolarını join ettik ve listesini döndük.
            using (NorthwindContext context = new NorthwindContext())
            {
                var result = from director in context.Directors
                             join movie in context.Movies
                             on director.DirectorID equals movie.DirectorID
                             select new DirectorDetailDto
                             {
                                 DirectorId = director.DirectorID,
                                 Name = director.Name,
                                 Surname = director.Surname,
                                 MovieName = movie.MovieName,
                                 MovieID = movie.MovieID,
                             };
                return result.ToList();
            }
        }
    }
}
