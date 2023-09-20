using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class BasketDetailDto
    {
        public int Id { get; set; }
        public int CustomerID { get; set; }
        public int MovieID { get; set; }
        public decimal MoviePrice { get; set; }
        public string MovieName { get; set; }
        public string CustomerFullName { get; set; }
        public DateTime DateOfAdding { get; set; }
    }
    
}
