using Hahn.ApplicationProcess.February2021.Domain.Assets;
using Hahn.ApplicationProcess.February2021.Domain.Exceptions;
using Hahn.ApplicationProcess.February2021.Domain.SeedWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Application.Assets.DeleteAsset
{
    public class DeleteAssetCommandHandler : IRequestHandler<DeleteAssetCommand>
    {
        private IUnitOfWork unityOfWork;

        public DeleteAssetCommandHandler(IUnitOfWork unityOfWork)
        {
            this.unityOfWork = unityOfWork;
        }

        public async Task<Unit> Handle(DeleteAssetCommand request, CancellationToken cancellationToken)
        {
            var asset = await unityOfWork.AssetRepository.FindAsset(request.Id);

            if (asset is null)
            {
                throw new EntityNotFoundException
                {
                    Resource = nameof(Asset)
                };
            }

            await unityOfWork.AssetRepository.DeleteAsset(asset);
            
            // Commit changes
            await unityOfWork.SaveChanges();

            return Unit.Value;
        }
    }
}
