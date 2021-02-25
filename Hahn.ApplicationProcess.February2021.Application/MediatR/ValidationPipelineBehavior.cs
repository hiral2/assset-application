using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Application.MediatR
{
    class ValidationPipelineBehavior<T, R> : IPipelineBehavior<T, R> where T : IRequest<R>
    {
        private readonly IEnumerable<IValidator> validators;

        public ValidationPipelineBehavior(IEnumerable<IValidator<T>> validators)
        {
            this.validators = validators;
        }

        public Task<R> Handle(T request, CancellationToken cancellationToken, RequestHandlerDelegate<R> next)
        {
            var context = new ValidationContext<T>(request);
            var failures = validators
                .Select(v => v.Validate(context))
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();
                
            if (failures.Any())
            {
                throw new FluentValidation.ValidationException(failures);
            }

            return next();
        }
    }
}
