using System;
using Swashbuckle.AspNetCore.Filters;
using Hahn.ApplicationProcess.February2021.Web.Controllers.Assets.Requests;

namespace Hahn.ApplicationProcess.February2021.Web.Controllers.Assets.Examples
{
    public class UpdateAssetRequestExample : IExamplesProvider<UpdateAssetRequest>
    {
        public UpdateAssetRequest GetExamples()
        {
            return new UpdateAssetRequest
            {
                AssetName = "TestAsset Updated",
                CountryOfDepartment = "Germany",
                EMailAddressOfDepartment = "rjhiral2@gmail.com",
                Broken = false,
                Department = "Store2",
                PurchaseDate = DateTime.UtcNow
            };
        }
    }
}
