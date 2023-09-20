using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class FavouriteValidator : AbstractValidator<AddFavourite>
    {
        //Product için kuralları burada belirteceğiz.
        public FavouriteValidator()
        {
            #region Favourite ekleme kontrolleri ve is kurallar
            RuleFor(p => p.MovieID).NotEmpty();
            RuleFor(p=>p.CustomerID).NotEmpty();    
            #endregion  
        }

    }

}

