using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfBasketDal : EfEntityRepositoryBase<Basket, NorthwindContext>, IBasketDal
    {
        public List<Basket> GetBasket()
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var result = from x in context.Baskets //BURADA KALDIK...
                             join user in context.Users
                             on x.CustomerID equals user.Id
                             //join movie in context.Basket
                             //on x.MovieID equals movie.MovieID
                             select new Basket
                             {
                                 CustomerID = user.Id,
                                 TotalPrice = x.TotalPrice,
                             };
                return result.ToList();
            }
        }
    }
}
