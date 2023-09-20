using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Basket : IEntity
    {
        public int Id { get; set; }
        public int CustomerID { get; set; }
        public decimal TotalPrice { get; set; }
    }
    public class basketAdd : IEntity
    {
        public int customerID { get; set; }
        public int movieID { get; set; }
    }
}
