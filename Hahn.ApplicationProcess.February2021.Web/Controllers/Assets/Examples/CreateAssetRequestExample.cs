using System;
using Swashbuckle.AspNetCore.Filters;
using Hahn.ApplicationProcess.February2021.Web.Controllers.Assets.Requests;

namespace Hahn.ApplicationProcess.February2021.Web.Controllers.Assets.Examples
{
    public class CreateAssetRequestExample : IExamplesProvider<CreateAssetRequest>
    {
        public CreateAssetRequest GetExamples()
        {
            return new CreateAssetRequest
            {
                AssetName = "TestAsset",
                CountryOfDepartment = "Germany",
                EMailAddressOfDepartment = "rjhiral2@gmail.com",
                Broken = false,
                Department = "Store1",
                PurchaseDate = DateTime.UtcNow
            };
        }
    }
}
