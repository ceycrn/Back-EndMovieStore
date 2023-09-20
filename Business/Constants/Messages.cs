using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        #region Movie Validation Rules
        public static string MovieNameAlreadyExists = "Movie name is already exists";
        public static string ListedAllMovies = "Listed all movies.";
        public static string MovieAdded = "Movie added succesfully";
        public static string MovieNotFound = "Movie doesn't exist which you wanted to delete.";
        public static string MovieDeleted = "Movie deleted.";
        public static string MovieYearIsInvalid = "Movie year is invalid.";
        public static string MovieYearIsValid = "Movie year is valid.";
        public static string MovieUpdated = "Movie updated successfully";
        public static string ListedOfPriceBetweenGiven = "Listed successfully betwwen given prices";
        public static string descendingOrder = "Movies ordered by descending price.";
        public static string ascendingOrder = "Movie ordered by descending price.";
        public static string searchingMoviesList = "Movies that constains searched words are listed.";
        public static string ListedJustFavouriteMovies = "Listed just users favourite movies successfully.";
        #endregion
        #region User Validation Rules
        public static string UserDeleted = "User deleted";
        public static string AuthorizationDenied = "You do not have the authority.";
        public static string UserRegistered = "User registered succesfully. ";
        public static string UserNotFound = "User doesn't exist.";
        public static string PasswordError = "Password error.";
        public static string SuccessfulLogin = "Successfull login.";
        public static string UserAlreadyExists = "This user already exists.";
        public static string AccessTokenCreated = "Access Token created.";
        public static string CustomerMovieNotFound = "Can not find given Movie or Customer ID.";
        public static string givenMovieAndCustomerAdded = "Given movie id and customer id added to DB";
        public static string CustomerAndMovieAlreadyExists = "Movie and Customer already existes";
        public static object EmailValidation = "Please enter a valid email address.";
        #endregion
        #region Director Validation Messages
        public static string DirectorAdded = "Director added successfully";
        public static string DirectorAlreadyAdded = "Director name has already added to the list";
        public static string ListedAllDirectors = "Listed all directors succesfully.";
        public static string DirectorAndMoviesListed = "Director and their movies listed";
        public static string DirectorDeleted = "Director deleted.";
        public static string DirectorNotFound = "Director doesn't exist.";
        #endregion
        #region Actor Validation Messages
        public static string ActorAdded = "Actor added successfully";
        public static string ActorAlreadyAdded = "Actor has already exists";
        public static string ActorDeleted = "Actor deleted successfully";
        public static string ActorNotFound = "Actor doesn't exist.";
        public static string ListedAllActors = "Actors listed successfully";
        public static string ListedAllMovieAndActor = "All listed";
        public static string ListedDetails = "All detail listed from MovieAndActor Table";
        public static string ListedJustActors = "Listed only this movie's actors";
        public static string MovieIDNotFound = "Can not find the searched movie";
        public static string ListedJustMovies = "Listed only this actor's films.";
        public static string MovieNameNotFound = "Can not find actors movie.";
        public static string ActorMovieNotFound = "Can not find given Movie or Actor ID.";
        public static string givenMovieAndActorAdded = "Given movie id and actor id added to DB";
        public static string ListedAllFavourites = "Listed all favourite table";
        public static string ListedAllDetail = "Listed all detail for favourite table";
        public static string AlreadyExists = "Movie and Actor already existes";
        #endregion
        #region Basket Validation Messages
        public static string ListedAllBasketDetail = "Listed all basket detail successfully.";
        public static string BasketAdded = "Added basket with success";
        public static string MovieOrCustomerIDFailed = "Not found customer or movie ID";
        public static string MovieinBasketDeleted = "Movie removed from basket successfully";
        public static string BasketIDNotFound = "Movie that you want to remove doesn't exist.";
        public static string ListedPersonelBasket = "Listed personel basket for given person with success";
        public static string ListedAllBasket = "Listed basket for given person with success";
        public static string CustomerAlreadyExists = "Customer already exists";
        public static string BuyIsSuccess = "Purchasing process is successfull.";
        public static string BudgetLimitExceed = "Budget limit exceeded";
        public static string BasketDetailDeleted = "Basket Detail that you choose successfully deleted.";
       
        
        #endregion

    }
}