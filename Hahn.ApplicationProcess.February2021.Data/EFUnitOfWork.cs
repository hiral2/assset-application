using Hahn.ApplicationProcess.February2021.Data.Domain.Assets;
using Hahn.ApplicationProcess.February2021.Data.Domain.Countries;
using Hahn.ApplicationProcess.February2021.Domain.Assets;
using Hahn.ApplicationProcess.February2021.Domain.Countries;
using Hahn.ApplicationProcess.February2021.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Data
{
    public class EFUnitOfWork : IUnitOfWork
    {
        public Lazy<IAssetRepository> lazyAssetRepository;
        public Lazy<ICountryRepository> lazyCountryRepository;
        private readonly February2021Context february2021Context;
        public IAssetRepository AssetRepository => lazyAssetRepository.Value;

        public ICountryRepository CountryRepository => lazyCountryRepository.Value;

        public EFUnitOfWork(February2021Context february2021Context, HttpCountryRepositoryOptions httpCountriesRepositoryOptions)
        {
            this.february2021Context = february2021Context;
            lazyAssetRepository = new Lazy<IAssetRepository>(() => new AssetRepository(this.february2021Context));
            lazyCountryRepository = new Lazy<ICountryRepository>(() => new HttpCountryRepository(httpCountriesRepositoryOptions));
        }

        public Task SaveChanges()
        {
            return february2021Context.SaveChangesAsync();
        }

    }
}
