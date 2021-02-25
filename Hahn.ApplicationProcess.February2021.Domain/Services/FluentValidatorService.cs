using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Domain.Services
{
    public class FluentValidatorService<T> : IValidatorService<T>
    {
        private readonly IEnumerable<IValidator<T>> validators;

        public FluentValidatorService(IEnumerable<IValidator<T>> validators)
        {
            this.validators = validators;
        }

        public Task ThrowIfNotValidAsync(T obj)
        {
            var context = new ValidationContext<T>(obj);
            var failures = validators
                .Select(v => v.Validate(context))
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Any())
            {
                throw new ValidationException(failures);
            }

            return Task.CompletedTask;
        }
    }
}
