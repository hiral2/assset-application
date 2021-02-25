using Autofac;
using AutoMapper;
using FluentValidation;
using Hahn.ApplicationProcess.February2021.Application.MediatR;
using Hahn.ApplicationProcess.February2021.Domain.Assets;
using Hahn.ApplicationProcess.February2021.Domain.SeedWork;
using Hahn.ApplicationProcess.February2021.Domain.Services;
using MediatR;
using MediatR.Pipeline;
using System;
using System.Linq;

namespace Hahn.ApplicationProcess.February2021.Application.AutofacModules
{
    public class ApplicationModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            var mediatorTypes = new[] {
                typeof(IRequestHandler<,>),
                typeof(IRequestHandler<>),
                typeof(IValidator<>)
            };

            foreach (var type in mediatorTypes)
            {
                builder
                    .RegisterAssemblyTypes(typeof(ApplicationModule).Assembly)
                    .AsClosedTypesOf(type)
                    .AsImplementedInterfaces();
            }

            builder
                  .RegisterAssemblyTypes(typeof(IUnitOfWork).Assembly)
                  .AsClosedTypesOf(typeof(IValidator<>))
                  .AsImplementedInterfaces();

            builder.Register<ServiceFactory>(ctx =>
            {
                var cc = ctx.Resolve<IComponentContext>();
                return t => cc.Resolve(t);
            });

            builder.RegisterType<MediatRFebruary2021Module>().AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(ValidationPipelineBehavior<,>)).As(typeof(IPipelineBehavior<,>));

            builder.RegisterAssemblyTypes(typeof(IMediator).Assembly)
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            builder.Register(context => new MapperConfiguration(cfg =>
            {
                var profileTypes = typeof(ApplicationModule).Assembly.GetTypes().Where(c => typeof(Profile).IsAssignableFrom(c));

                foreach(var profileType in profileTypes)
                {
                    cfg.AddProfile(Activator.CreateInstance(profileType) as Profile);
                }
            })).AsSelf().SingleInstance();

            builder.Register(c =>
            {
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
            .As<IMapper>()
            .InstancePerLifetimeScope();


            // Domain service
            builder.RegisterType<AssetService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(FluentValidatorService<>)).As(typeof(IValidatorService<>)).InstancePerLifetimeScope();

        }
    }
}
