using AutoMapper;
using Hahn.ApplicationProcess.February2021.Application.Contracts;
using Hahn.ApplicationProcess.February2021.Domain.Countries;
using Hahn.ApplicationProcess.February2021.Domain.Exceptions;
using Hahn.ApplicationProcess.February2021.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Application.Countries.FindCountry
{
    public class FindCountryQueryHandler : IQueryHandler<FindCountryQuery, CountryModel>
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public FindCountryQueryHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<CountryModel> Handle(FindCountryQuery request, CancellationToken cancellationToken)
        {
            var country = await unitOfWork.CountryRepository.FindCountry(request.Name);

            if (country is null)
            {
                throw new EntityNotFoundException
                {
                    Resource = nameof(Country)
                };
            }
            return mapper.Map<CountryModel>(country);
        }
    }
}
