using Hahn.ApplicationProcess.February2021.Domain.Countries;
using FluentValidation;
using Hahn.ApplicationProcess.February2021.Domain.SeedWork;

namespace Hahn.ApplicationProcess.February2021.Application.Assets.UpdateAsset
{
    public class UpdateAssetCommandValidator : BaseAssetAbstractValidator<UpdateAssetCommand>
    {
        public UpdateAssetCommandValidator(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        { 
        }

        public override void SetValidations()
        {
            RuleFor(c => c.Id).GreaterThan(0);
        }
    }
}
