using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Actor : IEntity
    {
        public int ActorID { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public int Age { get; set; }

        public Boolean Gender { get; set; }
    }

    public class AddActor : IEntity
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public int Age { get; set; }

        public Boolean Gender { get; set; }
    }
}
