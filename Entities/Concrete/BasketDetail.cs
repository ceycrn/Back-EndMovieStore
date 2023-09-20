using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class BasketDetail : IEntity
    {
        public int Id { get; set; }
        public int CustomerID { get; set; }
        public int MovieID { get; set; }
        public decimal MoviePrice { get; set; }
        public string MovieName { get; set; }
        public string CustomerFullName { get; set; }
        public DateTime DateOfAdding { get; set; }

    }
    public class addBasket : IEntity
    {
        public int customerID { get; set; }
        public int movieID { get; set; }
    }
 
}
