using Core.Entities.Concrete;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class MovieValidator : AbstractValidator<Movie>
    {
        //Product için kuralları burada belirteceğiz.
        public MovieValidator()
        {
            //    RuleFor(p => p.ProductName).NotEmpty(); //isim boş olamaz
            //    RuleFor(p => p.ProductName).MinimumLength(2); //Ürün adı min 2 karakter olmalı,
            //    RuleFor(p => p.UnitPrice).NotEmpty(); //Ürün fiyatı boş olamaz
            //    RuleFor(p => p.UnitPrice).GreaterThan(0); //Ürün fiyatı 0'dan büyük olmalı
            //    RuleFor(p => p.CategoryId).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1);//kategori ıd'si 1 olan ürünlerin fiyatı 10 veya daha fazla olmalı.
            //    RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("Product ismi A ile başlamali");
            //    //StartWithA bir fonksiyondur sen yazarsın ve burada o metodun şartını sağlamalasın zorunluluğu koyar.
            //    RuleFor(p => p.CategoryId).LessThanOrEqualTo(10);
            #region Movie ekleme kontrolleri ve is kurallar
            RuleFor(p => p.MovieID).NotEmpty();
            RuleFor(p => p.Price).GreaterThan(0);
            RuleFor(p => p.Price).NotEmpty();
            RuleFor(p => p.MovieName).NotEmpty();
            RuleFor(p => p.MovieYear).NotEmpty();
            RuleFor(p => p.MovieGenre).NotEmpty();
            #endregion  
        }

    }

}

