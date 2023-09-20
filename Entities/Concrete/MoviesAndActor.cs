using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class MoviesAndActor : IEntity
    {
        public int Id { get; set; }
        public int MovieID { get; set; }
        public int ActorID { get; set; }
    }
    public class AddMoviesAndActor : IEntity
    {
        public int MovieID { get; set; }
        public int ActorID { get; set; }
    }
  
}

