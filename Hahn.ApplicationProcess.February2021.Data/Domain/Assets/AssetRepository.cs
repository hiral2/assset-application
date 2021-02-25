using Hahn.ApplicationProcess.February2021.Domain.Assets;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Data.Domain.Assets
{
    public class AssetRepository : IAssetRepository
    {
        private readonly February2021Context february2021Context;

        public AssetRepository(February2021Context february2021Context)
        {
            this.february2021Context = february2021Context;
        }

        public Task CreateAsset(Asset asset)
        {
            return february2021Context.AddAsync(asset).AsTask();
        }

        public Task DeleteAsset(Asset asset)
        {
            february2021Context.Remove(asset);
            return Task.CompletedTask;
        }

        public Task<bool> ExistsAsset(int id)
        {
            return february2021Context.Assets.AnyAsync(c => c.Id == id);
        }

        public Task<Asset> FindAsset(int id)
        {
            return february2021Context.Assets.FirstOrDefaultAsync(a => a.Id == id);
        }

        public Task<List<Asset>> GetAssets()
        {
            return february2021Context.Assets.ToListAsync();
        }

        public Task UpdateAsset(Asset asset)
        {
            february2021Context.Update(asset);
            return Task.CompletedTask;
        }
    }
}
