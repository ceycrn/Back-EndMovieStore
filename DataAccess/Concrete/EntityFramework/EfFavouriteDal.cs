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
    public class EfFavouriteDal : EfEntityRepositoryBase<Favourite, NorthwindContext>, IFavouriteDal

    {
        public List<FavouriteDetailDto> GetFavouriteDetail()
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var result = from x in context.Favourites
                             join customer in context.Users
                             on x.CustomerID equals customer.Id
                             join movie in context.Movies
                             on x.MovieID equals movie.MovieID
                             select new FavouriteDetailDto
                             {
                                 Id = x.Id,
                                 MovieID = x.MovieID,
                                 MovieName = movie.MovieName,
                                 CustomerID = x.CustomerID,
                                 CustomerName = customer.FirstName,
                                 CustomerSurname = customer.LastName,
                                 
                             };
                return result.ToList();
            }
        }
    }
}
