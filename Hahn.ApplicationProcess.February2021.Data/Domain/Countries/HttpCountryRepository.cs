using Hahn.ApplicationProcess.February2021.Domain.Countries;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Data.Domain.Countries
{
    public class HttpCountryRepository : ICountryRepository
    {
        private readonly RestClient restClient;
        private readonly HttpCountryRepositoryOptions options;

        public HttpCountryRepository(HttpCountryRepositoryOptions options)
        {
            this.options = options;
            restClient = new RestClient(this.options.BaseUrl);
        }

        public async Task<Country> FindCountry(string name)
        {
            var request = new RestRequest("/rest/v2/name/{name}")
                                .AddUrlSegment("name", name)
                                .AddQueryParameter("fullText", true.ToString());
            var response = await restClient.ExecuteGetAsync<List<Country>>(request);

            if (!response.IsSuccessful)
            {
                return null;
            } 

            return response.Data?.FirstOrDefault();
        }

        public async Task<List<Country>> GetCountries(string name)
        {

            IRestRequest request = null;
            if (string.IsNullOrEmpty(name))
            {
                request = new RestRequest("/rest/v2");
            }
            else
            {
                request = new RestRequest("/rest/v2/name/{name}").AddUrlSegment("name", name);
            }
            var result = await restClient.GetAsync<List<Country>>(request);

            return result;
        }
    }
}
