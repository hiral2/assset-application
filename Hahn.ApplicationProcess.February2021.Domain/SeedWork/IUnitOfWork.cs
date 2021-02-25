using Hahn.ApplicationProcess.February2021.Domain.Assets;
using Hahn.ApplicationProcess.February2021.Domain.Countries;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Domain.SeedWork
{
    public interface IUnitOfWork
    {
        IAssetRepository AssetRepository { get; }
        ICountryRepository CountryRepository { get; }
        Task SaveChanges();
    }
}
