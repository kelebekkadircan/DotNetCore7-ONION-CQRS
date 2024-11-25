using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiApi.Application.Behaviors
{
    //public class FluentValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>  where TRequest : IRequest<TResponse>
    //{

    //    private readonly IEnumerable<IValidator<TRequest>> _validators;
    //    public FluentValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    //    {
    //           this._validators = validators;
    //    }

    //    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    //    {
    //        var context = new ValidationContext<TRequest>(request);
    //        var failtures = _validators
    //            .Select(v => v.Validate(context))
    //            .SelectMany(result => result.Errors)
    //            .GroupBy( f => f.ErrorMessage)
    //            .Select(f => f.First())
    //            .Where(f => f != null)
    //            .ToList();


    //        if (failtures.Any())
    //        {
    //            throw new ValidationException(failtures);

    //        }

    //        return next();



    //    }
    //}
    public class FluentValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public FluentValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators; // Doğrulayıcılar dependency injection ile alınır.
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // FluentValidation doğrulama bağlamı oluşturulur.
            var context = new ValidationContext<TRequest>(request);

            // Doğrulama işlemi yapılır.
            var failures = _validators
                .Select(v => v.Validate(context)) // Tüm doğrulayıcılar çalıştırılır.
                .SelectMany(result => result.Errors) // Tüm hata mesajları toplanır.
                .GroupBy(f => f.ErrorMessage) // Aynı hata mesajları gruplanır.
                .Select(f => f.First()) // Her grubun ilk hatası alınır.
                .Where(f => f != null) // Null hatalar filtrelenir.
                .ToList(); // Listeye dönüştürülür.

            // Eğer doğrulama hatası varsa işlem kesilir.
            if (failures.Any())
            {
                throw new ValidationException(failures);
            }

            // Doğrulama başarılıysa bir sonraki middleware'e geçilir.
            return await next();
        }
    }

}
