using Autofac;
using FluentValidation;
using FluentValidation.AspNetCore;
using Hahn.ApplicationProcess.February2021.Application.AutofacModules;
using Hahn.ApplicationProcess.February2021.Data.AutofacModules;
using Hahn.ApplicationProcess.February2021.Domain.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Diagnostics;
using System.Text.Json;

namespace Hahn.ApplicationProcess.Application
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            WebHostEnvironment = env;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (WebHostEnvironment.IsDevelopment())
            {
                services.AddCors(o =>
                {
                    o.AddDefaultPolicy(builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    });
                });
            }

            services.AddControllers().AddFluentValidation();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hahn.ApplicationProcess.Application", Version = "v1" });
                c.ExampleFilters();
            })
            .AddSwaggerExamplesFromAssemblyOf<Startup>();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var countryApiBaseurl = Configuration.GetSection("CountryApi").GetValue<string>("BaseUrl");
            builder.RegisterModule(new ApplicationModule());
            builder.RegisterModule(new DataModule(countryApiBaseurl));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseCors();
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hahn.ApplicationProcess.Application v1"));
            }
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = errorFeature.Error;

                    // https://tools.ietf.org/html/rfc7807#section-3.1
                    var problemDetails = new ProblemDetails
                    {
                        //Type = $"https://example.com/problem-types/{exception.GetType().Name}",
                        Title = "An unexpected error occurred!",
                        Detail = "Something went wrong",
                        Instance = errorFeature switch
                        {
                            ExceptionHandlerFeature e => e.Path,
                            _ => "unknown"
                        },
                        Status = StatusCodes.Status400BadRequest,
                        Extensions =
            {
                ["trace"] = Activity.Current?.Id ?? context?.TraceIdentifier
            }
                    };

                    switch (exception)
                    {
                        case ValidationException validationException:
                            problemDetails.Status = StatusCodes.Status400BadRequest;
                            problemDetails.Title = "One or more validation errors occurred";
                            problemDetails.Detail = "The request contains invalid parameters. More information can be found in the errors.";
                            problemDetails.Extensions["errors"] = validationException.Errors;
                            break;
                        case EntityNotFoundException enfe:
                            problemDetails.Status = StatusCodes.Status404NotFound;
                            problemDetails.Title = "Not found";
                            problemDetails.Detail = $"Resource {enfe.Resource} not found";
                            break;
                    }

                    context.Response.ContentType = "application/problem+json";
                    context.Response.StatusCode = problemDetails.Status.Value;
                    context.Response.GetTypedHeaders().CacheControl = new CacheControlHeaderValue()
                    {
                        NoCache = true,
                    };
                    await JsonSerializer.SerializeAsync(context.Response.Body, problemDetails);
                });
            });
            

            app.UseHttpsRedirection();

            app.UseDefaultFiles();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
