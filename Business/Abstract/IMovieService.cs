using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IMovieService
    {
        IDataResult<List<Movie>> GetAll();

        IResult Add(AddMovie movie);

        IResult Remove(string movieName);

        IResult Update(Movie movie);
        IDataResult<List<Movie>> getPriceBetween(int min, int max);

        IDataResult<List<Movie>>  OrderPriceByDescending();

        IDataResult<List<Movie>> OrderPriceByAscending();

        IDataResult<List<Movie>> Search(string MovieName);
    }
}
