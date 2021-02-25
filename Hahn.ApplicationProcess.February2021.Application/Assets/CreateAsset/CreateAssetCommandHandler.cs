using AutoMapper;
using Hahn.ApplicationProcess.February2021.Domain.Assets;
using Hahn.ApplicationProcess.February2021.Domain.SeedWork;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Application.Assets.CreateAsset
{
    public class CreateAssetCommandHandler : IRequestHandler<CreateAssetCommand, AssetModel>
    {
        private readonly IAssetService assetService;
        private readonly IMapper mapper;

        public CreateAssetCommandHandler(
            IMapper mapper,
            IAssetService assetService
            )
        {
            this.assetService = assetService;
            this.mapper = mapper;
        }

        public async Task<AssetModel> Handle(CreateAssetCommand request, CancellationToken cancellationToken)
        {
            System.Enum.TryParse(request.Department, out Department department);
            var asset = new Asset
            {
                AssetName = request.AssetName,
                Broken = request.Broken,
                CountryOfDepartment = request.CountryOfDepartment,
                Department = department,
                EMailAddressOfDepartment = request.EMailAddressOfDepartment,
                PurchaseDate = request.PurchaseDate
            };
            
            await assetService.CreateAsset(asset);

            return mapper.Map<AssetModel>(asset);
        }
    }
}
