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
    public interface IMovieAndActorService
    {
        IDataResult<List<MoviesAndActor>> GetAll();
        //BURASI DETAİL DTO OLARAK DÖNECEK VE O ID VE FILME AIT BİLGİLER EKLENECEK.
        IDataResult<List<MovieAndActorDetailDto>> GetDetail();
        // Sadece o filme ait tüm oyuncular (film id ile getirilecek)
        //Sadece o oyuncuya ait tüm filmler(oyuncu id'den çekilecek)
        //IDataResult <List<JustActors>> GetJustActorsFromMovie(int movieID);
        IDataResult<List<JustActors>> GetJustActors(int id);

        IDataResult<List<JustMovies>> GetJustMovies(int id);
        IResult Add(int actorID, int movieID);
    }
}
