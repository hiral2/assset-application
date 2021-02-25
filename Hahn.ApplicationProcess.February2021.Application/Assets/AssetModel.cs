using System;
using System.Collections.Generic;
using System.Text;

namespace Hahn.ApplicationProcess.February2021.Application.Assets
{
    public class AssetModel
    {
        public int Id { get; set; }
        public string AssetName { get; set; }
        public string Department { get; set; }
        public string CountryOfDepartment { get; set; }
        public string EMailAddressOfDepartment { get; set; }
        public DateTime PurchaseDate { get; set; }
        public bool Broken { get; set; }
    }
}
