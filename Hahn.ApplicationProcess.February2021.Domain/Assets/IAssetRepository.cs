using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Domain.Assets
{
    public interface IAssetRepository
    {
        Task<List<Asset>> GetAssets();
        Task<Asset> FindAsset(int id);
        Task<bool> ExistsAsset(int id);
        Task DeleteAsset(Asset asset);
        Task CreateAsset(Asset asset);
        Task UpdateAsset(Asset asset);
    }
}
