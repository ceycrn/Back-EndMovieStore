using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class MovieDetailDto: IDto
    {
        public int MovieID { get; set; } 

        public string MovieName { get; set; }

        public string DirectorName { get; set; }


    }
}
