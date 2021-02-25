
using System;

namespace Hahn.ApplicationProcess.February2021.Web.Controllers.Assets.Requests
{
    public class CreateAssetRequest
    {
        public string AssetName { get; set; }
        public string Department { get; set; }
        public string CountryOfDepartment { get; set; }
        public string EMailAddressOfDepartment { get; set; }
        public DateTime PurchaseDate { get; set; }
        public bool Broken { get; set; }
    }
}
