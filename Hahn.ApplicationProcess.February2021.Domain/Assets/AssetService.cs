using System;
using System.Threading.Tasks;
using Hahn.ApplicationProcess.February2021.Domain.Exceptions;
using Hahn.ApplicationProcess.February2021.Domain.SeedWork;
using Hahn.ApplicationProcess.February2021.Domain.Services;

namespace Hahn.ApplicationProcess.February2021.Domain.Assets
{
    public class AssetService : IAssetService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IValidatorService<Asset> validatorService;

        public AssetService(
            IUnitOfWork unitOfWork,
            IValidatorService<Asset> validatorService
            )
        {
            this.unitOfWork = unitOfWork;
            this.validatorService = validatorService;
        }

        public async Task CreateAsset(Asset asset)
        {
            if (asset is null)
            {
                throw new ArgumentNullException(nameof(asset));
            }

            await validatorService.ThrowIfNotValidAsync(asset);
            await unitOfWork.AssetRepository.CreateAsset(asset);
            await unitOfWork.SaveChanges();
        }

        public async Task UpdateAsset(Asset assetData)
        {
            if (assetData is null)
            {
                throw new ArgumentNullException(nameof(assetData));
            }

            var asset = await unitOfWork.AssetRepository.FindAsset(assetData.Id);
            if (asset is null)
            {
                throw new EntityNotFoundException
                {
                    Resource = nameof(Asset)
                };
            }

            await validatorService.ThrowIfNotValidAsync(asset);

            asset.AssetName = assetData.AssetName;
            asset.Broken = assetData.Broken;
            asset.CountryOfDepartment = assetData.CountryOfDepartment;
            asset.Department = assetData.Department;
            asset.EMailAddressOfDepartment = assetData.EMailAddressOfDepartment;
            asset.PurchaseDate = assetData.PurchaseDate;

            await unitOfWork.AssetRepository.UpdateAsset(asset);
            await unitOfWork.SaveChanges();
        }
    }
}
