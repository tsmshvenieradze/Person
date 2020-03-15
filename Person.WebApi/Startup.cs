using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Person.Domain;
using Person.Domain.Contracts;
using Person.Domain.Services;
using Person.Infrastructure.Db;
using Person.Infrastructure.Repositories;
using Person.WebApi.Filters;
using Person.WebApi.Middlewares;

namespace Person.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddLocalization(options => options.ResourcesPath = "Resources");
           
            services.Configure<RequestLocalizationOptions>(options =>
            {
                //...

                options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(context =>
                {
                    //...
                    var userLangs = context.Request.Headers["Accept-Language"].ToString();
                    var firstLang = userLangs.Split(',').FirstOrDefault();
                    var defaultLang = string.IsNullOrEmpty(firstLang) ? "en" : firstLang;
                    return Task.FromResult(new ProviderCultureResult(defaultLang, defaultLang));
                }));
            });
            services.AddDbContext<PersonDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("PersonDbConnection")));

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<ILoggerRepository, LoggerRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IPhoneNumberRepository, PhoneNumberRepository>();
            services.AddScoped<IPersonRelationRepository, PersonRelationRepository>();
            services.AddScoped<RepositoryProvider>();
            services.AddScoped<PersonService>();
            services.AddScoped<ValidationFilterAttribute>();
           
            services.AddLocalization(o =>
            {
                // We will put our translations in a folder called Resources
                o.ResourcesPath = "Resources";
            });



            services.AddSwaggerGenNewtonsoftSupport(); // explicit opt-in - needs to be placed after AddSwaggerGen()
            var sp = services.BuildServiceProvider();
            var db = sp.GetService(typeof(PersonDbContext)) as PersonDbContext;
            db.Database.EnsureCreated();

            var initializer = new DbInitializer();
            initializer.Seed(db);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Person Api",


                }); 

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Person API V1");
            });
            app.UseHttpsRedirection();
            app.UseRouting();

            app.ExceptionLog();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
