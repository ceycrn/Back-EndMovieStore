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
    public class EfBasketDetailDal : EfEntityRepositoryBase<BasketDetail, NorthwindContext>, IBasketDetailDal
    {
        public List<BasketDetail> GetBasketDetail()
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var result = from x in context.BasketDetail
                             join user in context.Users
                             on x.CustomerID equals user.Id
                             join movie in context.Movies
                             on x.MovieID equals movie.MovieID
                             select new BasketDetail
                             { 
                                 CustomerID = user.Id,
                                 MovieID = movie.MovieID,
                                 MoviePrice = movie.Price,
                                 MovieName = movie.MovieName,
                                 CustomerFullName = user.FirstName + " " +user.LastName,
                                 DateOfAdding = DateTime.Now,
                             };
                return result.ToList();
            }
        }
    }
}
