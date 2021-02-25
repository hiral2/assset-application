using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Domain.Exceptions
{
    public class EntityNotFoundException: Exception
    {
        public string Resource { get; set; }
    }
}
