using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Director : IEntity
    {
        public int DirectorID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
    public class AddDirector : IEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }

}
