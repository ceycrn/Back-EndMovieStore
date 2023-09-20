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
    public interface IFavouriteService
    {
        IDataResult<List<Favourite>> GetAll();

        IDataResult<List<FavouriteDetailDto>> GetDetail();

        IResult Add(int customerID, int movieID);

        IDataResult<List<JustMovies>> GetJustFavouriteMovies(int id);

        ////IDataResult <List<JustActors>> GetJustActorsFromMovie(int movieID);
        ///
        //IDataResult<List<JustActors>> GetJustActors(int id);

        //IDataResult<List<JustMovies>> GetJustMovies(int id);

        //IResult Add(int actorID, int movieID);
    }
}
