using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class DirectorDetailDto
    {
        public int DirectorId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MovieName { get; set; }
        public int MovieID { get; set; }

      
    }
}
