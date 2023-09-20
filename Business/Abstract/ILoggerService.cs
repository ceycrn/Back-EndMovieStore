using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ILoggerService
    {
        public void DoLog<T>(T variable);

        //public void PreLog<T>(Type type, T variable);
    }

    
}
