using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        //Product için kuralları burada belirteceğiz.
        public CustomerValidator()
        {
            //    RuleFor(p => p.ProductName).NotEmpty(); //isim boş olamaz
            //    RuleFor(p => p.ProductName).MinimumLength(2); //Ürün adı min 2 karakter olmalı,
            //    RuleFor(p => p.UnitPrice).NotEmpty(); //Ürün fiyatı boş olamaz
            //    RuleFor(p => p.UnitPrice).GreaterThan(0); //Ürün fiyatı 0'dan büyük olmalı
            //    RuleFor(p => p.CategoryId).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1);//kategori ıd'si 1 olan ürünlerin fiyatı 10 veya daha fazla olmalı.
            //    RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("Product ismi A ile başlamali");
            //    //StartWithA bir fonksiyondur sen yazarsın ve burada o metodun şartını sağlamalasın zorunluluğu koyar.
            //    RuleFor(p => p.CategoryId).LessThanOrEqualTo(10);
            #region User ekleme kontrolleri ve is kurallar
            RuleFor(p => p.FirstName).MinimumLength(3);
            RuleFor(p => p.FirstName).MaximumLength(25);
            RuleFor(p => p.LastName).MinimumLength(1);
            RuleFor(p => p.LastName).MaximumLength(25);
            RuleFor(p => p.FirstName).NotEmpty();
            RuleFor(p => p.LastName).NotEmpty();

            #endregion  
        }

    }

}

