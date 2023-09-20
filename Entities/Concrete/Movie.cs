using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Movie : IEntity
    {
        public int MovieID { get; set; }

        public string MovieName { get; set; }

        public int MovieYear { get; set; }

        public string MovieGenre { get; set; }

        public decimal Price { get; set; }

        public int DirectorID { get; set; }
    }
    public class AddMovie : IEntity
    {

        public string MovieName { get; set; }

        public int MovieYear { get; set; }

        public string MovieGenre { get; set; }

        public decimal Price { get; set; }

        public int DirectorID { get; set; }
    }
}
