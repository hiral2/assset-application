using AutoMapper;
using Hahn.ApplicationProcess.February2021.Domain.Countries;

namespace Hahn.ApplicationProcess.February2021.Application.Countries
{
    public class CountryModelProfile: Profile
    {
        public CountryModelProfile()
        {
            CreateMap<Country, CountryModel>();
        }
    }
}
