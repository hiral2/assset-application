using Hahn.ApplicationProcess.February2021.Application.Assets;
using Hahn.ApplicationProcess.February2021.Application.Assets.CreateAsset;
using Hahn.ApplicationProcess.February2021.Application.Assets.DeleteAsset;
using Hahn.ApplicationProcess.February2021.Application.Assets.FindAsset;
using Hahn.ApplicationProcess.February2021.Application.Assets.GetAllAssets;
using Hahn.ApplicationProcess.February2021.Application.Assets.UpdateAsset;
using Hahn.ApplicationProcess.February2021.Application.Contracts;
using Hahn.ApplicationProcess.February2021.Web.Controllers.Assets.Examples;
using Hahn.ApplicationProcess.February2021.Web.Controllers.Assets.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Web.Controllers.Assets
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController: ControllerBase
    {
        private readonly IFebruary2021Module module;
        public AssetsController(IFebruary2021Module module)
        {
            this.module = module;
        }

        [HttpGet]
        public Task<IEnumerable<AssetModel>> GetAssets()
        {
            return module.ExecuteQueryAsync(new GetAllAssetsQuery());
        }

        [HttpGet("{id}")]
        public Task<AssetModel> GetAsset(int id)
        {
            return module.ExecuteQueryAsync(new FindAssetQuery { Id = id });
        }

        [HttpDelete("{id}")]
        public Task DeleteAsset(int id)
        {
            return module.ExecuteCommandAsync(new DeleteAssetCommand { Id = id });
        }

        [HttpPost]
        public async Task<IActionResult> PostAsset([FromBody] CreateAssetRequest createAssetRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await module.ExecuteCommandAsync<AssetModel>(new CreateAssetCommand {
                AssetName = createAssetRequest.AssetName,
                Broken = createAssetRequest.Broken,
                CountryOfDepartment = createAssetRequest.CountryOfDepartment,
                Department = createAssetRequest.Department,
                EMailAddressOfDepartment = createAssetRequest.EMailAddressOfDepartment,
                PurchaseDate = createAssetRequest.PurchaseDate
            });

            return CreatedAtAction(nameof(GetAsset), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsset(int id, [FromBody]UpdateAssetRequest updateAssetRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await module.ExecuteCommandAsync(new UpdateAssetCommand
            {
                Id = id,
                AssetName = updateAssetRequest.AssetName,
                Broken = updateAssetRequest.Broken,
                CountryOfDepartment = updateAssetRequest.CountryOfDepartment,
                Department = updateAssetRequest.Department,
                EMailAddressOfDepartment = updateAssetRequest.EMailAddressOfDepartment,
                PurchaseDate = updateAssetRequest.PurchaseDate
            });

            return Ok();
        }

    }
}
