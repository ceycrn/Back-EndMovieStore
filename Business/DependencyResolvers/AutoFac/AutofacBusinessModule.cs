using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module //Module olarak api startup'ta yapılan şu sınıf şuradan alır yapısını buraya taşıyoruz autofac modulü ile
    {
        //override ile ezilecek metodları buluyoruz buradan Load yapısı ile
        //uygulama çalıştığı an override kısmı da çalıaşcak
        protected override void Load(ContainerBuilder builder)
        {
            ////services.AddSingleton<IProductDal, EfProductDal>();
            ////startup.cs web api de yazdığımız kodun services.AddSingleton<IProductService, ProductManager>(); karşılığı aşagıdaki kod blogudur. Böylelikle autofac yapısını kullanmış oluyoruz.
            builder.RegisterType<MovieManager>().As<IMovieService>().SingleInstance();
            builder.RegisterType<EfMovieDal>().As<IMovieDal>().SingleInstance();

            builder.RegisterType<DirectorManager>().As<IDirectorService>().SingleInstance();
            builder.RegisterType<EfDirectorDal>().As<IDirectorDal>().SingleInstance();

            builder.RegisterType<ActorManager>().As<IActorService>().SingleInstance();
            builder.RegisterType<EfActorDal>().As<IActorDal>().SingleInstance();

            builder.RegisterType<BasketManager>().As<IBasketService>().SingleInstance();
            builder.RegisterType<EfBasketDal>().As<IBasketDal>().SingleInstance();

            builder.RegisterType<BasketDetailManager>().As<IBasketDetailService>().SingleInstance();
            builder.RegisterType<EfBasketDetailDal>().As<IBasketDetailDal>().SingleInstance();

            builder.RegisterType<FavouriteManager>().As<IFavouriteService>().SingleInstance();
            builder.RegisterType<EfFavouriteDal>().As<IFavouriteDal>().SingleInstance();

            builder.RegisterType<MovieAndActorManager>().As<IMovieAndActorService>().SingleInstance();
            builder.RegisterType<EfMovieAndActorDal>().As<IMovieAndActorDal>().SingleInstance();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();
            builder.RegisterType<EfUserDal>().As<IUserDal>().SingleInstance();

            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>();//hata alınan yer ancak bende hata almadı

            //bir kere singleInstance üretir ve her kullanıcıyla bunu paylaşır.
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}