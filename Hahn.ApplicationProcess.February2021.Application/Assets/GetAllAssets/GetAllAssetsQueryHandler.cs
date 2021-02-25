using AutoMapper;
using Hahn.ApplicationProcess.February2021.Application.Contracts;
using Hahn.ApplicationProcess.February2021.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Application.Assets.GetAllAssets
{
    public class GetAllAssetsQueryHandler : IQueryHandler<GetAllAssetsQuery, IEnumerable<AssetModel>>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public GetAllAssetsQueryHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<AssetModel>> Handle(GetAllAssetsQuery request, CancellationToken cancellationToken)
        {
            var assets = await unitOfWork.AssetRepository.GetAssets();
            return mapper.Map<List<AssetModel>>(assets);
        }
    }
}
