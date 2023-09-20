using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class MovieAndActorDetailDto
    {
        public int Id { get; set; }
        public int MovieID { get; set; }
        public string MovieName { get; set; }
        public int ActorID { get; set; }
        public string ActorName { get; set; }
        public string ActorSurname { get; set; }

    }
    public class JustActors
    {
        public string FullName { get; set; }
    }

    public class JustMovies
    {
        public string MovieName { get; set; }
    }

}
