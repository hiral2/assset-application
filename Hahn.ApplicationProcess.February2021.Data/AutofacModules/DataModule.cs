using Autofac;
using Hahn.ApplicationProcess.February2021.Data.Domain.Countries;
using Microsoft.EntityFrameworkCore;

namespace Hahn.ApplicationProcess.February2021.Data.AutofacModules
{
    public class DataModule: Module
    {
        private readonly string countryApiBaseUrl;

        public DataModule(string countryApiBaseUrl)
        {
            this.countryApiBaseUrl = countryApiBaseUrl;
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterType<EFUnitOfWork>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.Register(c => {
                var opt = new DbContextOptionsBuilder<February2021Context>();
                opt.UseInMemoryDatabase(nameof(February2021Context));
                return new February2021Context(opt.Options);
            }).AsSelf().InstancePerLifetimeScope();

            builder.Register(c => new HttpCountryRepositoryOptions
            {
                BaseUrl = countryApiBaseUrl
            }).AsSelf().SingleInstance();
        }
    }
}
