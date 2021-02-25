using AutoMapper;
using Hahn.ApplicationProcess.February2021.Domain.Assets;
using Hahn.ApplicationProcess.February2021.Domain.Exceptions;
using Hahn.ApplicationProcess.February2021.Domain.SeedWork;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Application.Assets.FindAsset
{
    public class FindAssetQueryHandler : IRequestHandler<FindAssetQuery, AssetModel>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public FindAssetQueryHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<AssetModel> Handle(FindAssetQuery request, CancellationToken cancellationToken)
        {
            var asset = await unitOfWork.AssetRepository.FindAsset(request.Id);

            if (asset is null)
            {
                throw new EntityNotFoundException
                {
                    Resource = nameof(Asset)
                };
            }

            return mapper.Map<AssetModel>(asset);
        }
    }
}
