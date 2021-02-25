using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Domain.Assets
{
    public interface IAssetService
    {
        Task CreateAsset(Asset asset);
        Task UpdateAsset(Asset asset);
    }
}
