using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IDirectorService
    {
        IResult Add(AddDirector director);
        IDataResult<List<Director>> GetAll();
        IDataResult<List<DirectorDetailDto>> GetAllMovies(int id);
        IResult Delete(int id);
    }
}
