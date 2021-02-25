using Hahn.ApplicationProcess.February2021.Application.Contracts;

namespace Hahn.ApplicationProcess.February2021.Application.Assets.DeleteAsset
{
    public class DeleteAssetCommand: ICommand
    {
        public int Id { get; init; }
    }
}
