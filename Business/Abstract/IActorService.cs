using Core.Utilities.Results;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IActorService
    {
        IResult Add(AddActor actor);
        IResult Delete(int id);
        IDataResult<List<Actor>> GetAll();  
    }
}