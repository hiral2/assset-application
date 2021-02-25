using Hahn.ApplicationProcess.February2021.Domain.Assets;
using Microsoft.EntityFrameworkCore;

namespace Hahn.ApplicationProcess.February2021.Data
{
    public class February2021Context: DbContext
    {
        public DbSet<Asset> Assets { get; set; }

        public February2021Context(DbContextOptions<February2021Context> options)
            :base(options)
        {
        }
    }
}
