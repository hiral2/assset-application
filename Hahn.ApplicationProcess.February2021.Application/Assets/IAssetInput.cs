using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Application.Assets
{
    public interface IAssetInput
    {
        public string AssetName { get; init; }
        public string Department { get; init; }
        public string CountryOfDepartment { get; init; }
        public string EMailAddressOfDepartment { get; init; }
        public bool Broken { get; init; }
    }
}
