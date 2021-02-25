using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Domain.Countries
{
    public interface ICountryRepository
    {
        public Task<List<Country>> GetCountries(string name);
        public Task<Country> FindCountry(string name);
    }
}
