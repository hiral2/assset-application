using Hahn.ApplicationProcess.February2021.Application.Contracts;

namespace Hahn.ApplicationProcess.February2021.Application.Assets.FindAsset
{
    public class FindAssetQuery: IQuery<AssetModel>
    {
        public int Id { get; init; }
    }
}
