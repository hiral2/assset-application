using AutoMapper;
using Hahn.ApplicationProcess.February2021.Application.Contracts;
using Hahn.ApplicationProcess.February2021.Domain.Countries;
using Hahn.ApplicationProcess.February2021.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Application.Countries.SearchContries
{
    public class SearchCountriesQueryHandler : IQueryHandler<SearchContriesQuery, List<CountryModel>>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public SearchCountriesQueryHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<CountryModel>> Handle(SearchContriesQuery request, CancellationToken cancellationToken)
        {
            var assets = await unitOfWork.CountryRepository.GetCountries(request.Name ?? "");
            return mapper.Map<List<CountryModel>>(assets);
        }
    }
}
