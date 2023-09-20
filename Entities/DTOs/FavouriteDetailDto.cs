using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class FavouriteDetailDto
    {
        public int Id { get; set; }
        public int MovieID { get; set; }
        public string MovieName { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
    }

    public class JustFavourite
    {
        public string MovieName { get; set; }
    }
}
