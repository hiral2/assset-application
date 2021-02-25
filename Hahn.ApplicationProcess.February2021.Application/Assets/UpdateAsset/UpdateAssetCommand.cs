

using Hahn.ApplicationProcess.February2021.Application.Contracts;
using System;

namespace Hahn.ApplicationProcess.February2021.Application.Assets.UpdateAsset
{
    public class UpdateAssetCommand : ICommand, IAssetInput
    {
        public int Id { get; init; }
        public string AssetName { get; init; }
        public string Department { get; init; }
        public string CountryOfDepartment { get; init; }
        public string EMailAddressOfDepartment { get; init; }
        public DateTime PurchaseDate { get; set; }
        public bool Broken { get; init; }
    }
}
