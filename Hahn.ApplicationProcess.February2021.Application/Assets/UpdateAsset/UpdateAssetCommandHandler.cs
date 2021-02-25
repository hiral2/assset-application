using Hahn.ApplicationProcess.February2021.Application.Contracts;
using Hahn.ApplicationProcess.February2021.Domain.Assets;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Application.Assets.UpdateAsset
{
    public class UpdateAssetCommandHandler : ICommandHandler<UpdateAssetCommand>
    {
        private readonly IAssetService assetService;

        public UpdateAssetCommandHandler(
            IAssetService assetService
            )
        {
            this.assetService = assetService;
        }

        public async Task<Unit> Handle(UpdateAssetCommand request, CancellationToken cancellationToken)
        {
            Enum.TryParse(request.Department, out Department department);
            var asset = new Asset
            {
                Id = request.Id,
                AssetName = request.AssetName,
                CountryOfDepartment = request.CountryOfDepartment,
                Department = department,
                EMailAddressOfDepartment = request.EMailAddressOfDepartment,
                Broken = request.Broken,
                PurchaseDate = request.PurchaseDate
            };

            await assetService.UpdateAsset(asset);
            
            return Unit.Value;
        }
    }
}
