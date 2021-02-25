using System;
using System.Collections.Generic;
using System.Text;

namespace Hahn.ApplicationProcess.February2021.Domain.Assets
{
    public class Asset
    {
        public int Id { get; set; }
        public string AssetName { get; set; }
        public Department Department { get; set; }
        public string CountryOfDepartment { get; set; }
        public string EMailAddressOfDepartment { get; set; }
        public DateTime PurchaseDate { get; set; }
        public bool Broken { get; set; }
    }
}
