using FluentValidation;
using Hahn.ApplicationProcess.February2021.Domain.Assets;
using Hahn.ApplicationProcess.February2021.Domain.Countries;
using Hahn.ApplicationProcess.February2021.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Application.Assets.CreateAsset
{
    public class CreateAssetCommandValidator: BaseAssetAbstractValidator<CreateAssetCommand>
    {
        public CreateAssetCommandValidator(IUnitOfWork unitOfWork)
            : base(unitOfWork) 
        {}
    }
}
