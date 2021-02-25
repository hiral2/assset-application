using Hahn.ApplicationProcess.February2021.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Application.Countries.FindCountry
{
    public class FindCountryQuery: IQuery<CountryModel>
    {
        public string Name { get; set; }
    }
}
