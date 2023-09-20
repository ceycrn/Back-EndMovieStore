using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class ActorValidator : AbstractValidator<AddActor>
    {
        //Product için kuralları burada belirteceğiz.
        public ActorValidator()
        {
            #region Actor ekleme kontrolleri ve is kurallar
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Surname).NotEmpty();
            RuleFor(p => p.Name).MaximumLength(25);
            RuleFor(p => p.Surname).MaximumLength(20);
            RuleFor(p => p.Age).GreaterThan(0);
            RuleFor(p => p.Age).LessThan(120);
            #endregion  
        }

    }

}

