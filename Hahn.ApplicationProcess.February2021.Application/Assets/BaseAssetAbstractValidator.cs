
using System;
using FluentValidation;
using Hahn.ApplicationProcess.February2021.Domain.Assets;
using Hahn.ApplicationProcess.February2021.Domain.Countries;
using Hahn.ApplicationProcess.February2021.Domain.SeedWork;

namespace Hahn.ApplicationProcess.February2021.Application.Assets
{
    public abstract class BaseAssetAbstractValidator<T>: AbstractValidator<T> where T: IAssetInput
    {
        public BaseAssetAbstractValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(c => c.AssetName).NotEmpty().MinimumLength(5).MaximumLength(100);
            RuleFor(c => c.Department).Custom((c, context) => {
                if (!Enum.TryParse(c, out Department d))
                {
                    context.AddFailure("invalid_department");
                }
            });
            RuleFor(c => c.EMailAddressOfDepartment).EmailAddress();
            SetValidations();
        }

        public virtual void SetValidations()
        { }
    }
}
