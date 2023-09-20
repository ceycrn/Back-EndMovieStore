using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception //Aspect miz MethodInterception (Aspect = Methodun başında sonunda ortasında neresinde istersek hata verdiğinde çalışacak yapı)
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("Bu bir dogrulama sınıfı degil...");
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation) //OnBefore methodunu eziyoruz (MethodInterception'daki method)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);//Çalışma anında bir instance olusturmak istersen Activator.CreateInstance kullanılır. Instance => Product p = new Product();
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}