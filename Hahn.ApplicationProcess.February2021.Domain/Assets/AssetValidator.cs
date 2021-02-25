using FluentValidation;
using Hahn.ApplicationProcess.February2021.Domain.SeedWork;
using Hahn.ApplicationProcess.February2021.Domain.SharedKernel;

namespace Hahn.ApplicationProcess.February2021.Domain.Assets
{
    public class AssetValidator: AbstractValidator<Asset> 
    {
        public AssetValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(c => c.AssetName).NotEmpty().MinimumLength(5).MaximumLength(100);
            RuleFor(c => c.CountryOfDepartment).NotEmpty().CustomAsync(async (c, context, cancellationToken) =>
            {
                var countryRepository = unitOfWork.CountryRepository;
                var result = await countryRepository.FindCountry(c);
                if (result is null)
                {
                    context.AddFailure("invalid_department_country");
                }
            });
            RuleFor(c => c.EMailAddressOfDepartment).EmailAddress();
            RuleFor(c => c.PurchaseDate).GreaterThanOrEqualTo((a) => SystemClock.Now.AddYears(-1)).WithMessage("invalid_purchase_date_year");
        }
    }
}
