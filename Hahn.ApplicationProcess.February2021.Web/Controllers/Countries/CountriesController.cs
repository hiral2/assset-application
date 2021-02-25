using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Hahn.ApplicationProcess.February2021.Application.Contracts;
using Hahn.ApplicationProcess.February2021.Application.Countries;
using Hahn.ApplicationProcess.February2021.Application.Countries.FindCountry;
using Hahn.ApplicationProcess.February2021.Application.Countries.SearchContries;

namespace Hahn.ApplicationProcess.February2021.Web.Controllers.Countries
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController : ControllerBase
    {
        private readonly IFebruary2021Module module;
        public CountriesController(IFebruary2021Module module)
        {
            this.module = module;
        }

        [HttpGet]
        public Task<List<CountryModel>> GetCountries([FromQuery] string name)
        {
            return module.ExecuteQueryAsync<List<CountryModel>>(new SearchContriesQuery
            {
                Name = name
            });
        }

        [HttpGet("{name}")]
        public Task<CountryModel> GetCountry( string name)
        {
            return module.ExecuteQueryAsync<CountryModel>(new FindCountryQuery
            {
                Name = name
            });
        }



    }
}
