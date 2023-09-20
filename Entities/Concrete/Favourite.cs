using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Favourite : IEntity
    {
        public int Id { get; set; }
        public int CustomerID { get; set; }
        public int MovieID { get; set; }
    }
    public class AddFavourite : IEntity
    {
        public int CustomerID { get; set; }
        public int MovieID { get; set; }
    }
}
