using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PManagement.API.Extensions;
using PManagement.Core.Infrastructure.UnitOfWork;
using PManagement.Core.Interfaces.Repositories;
using PManagement.Core.Interfaces.Services;
using PManagement.Core.Services;
using PManagement.DataProvider;
using PManagement.DataProvider.Repository;
using PManagement.Storage;

namespace PManagement.API
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
            //services.AddDbContext

            //services.AddSingleton<ICacheProvider>(provider => new RedisCacheProvider("myPrettyLocalhost:6379"));
            var storageSection = Configuration.GetSection("Azure:Storage");
            services.AddSingleton<IStorageService>(new AzureStorage(storageSection.GetValue<string>("StorageAccount"), 
                storageSection.GetValue<string>("StorageKey"), storageSection.GetValue<string>("ContainerName")));

            services.AddDbContext<PManagementContext>(options => options.UseSqlServer(Configuration.GetConnectionString("PManagementDatabase")));
            services
                .AddScoped<IAuthenticationService, AuthenticationService>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<ITokenInfoRepository, TokenInfoRepository>()
                .AddScoped<ICompanyRepository, CompanyRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IUnitOfWork, PManagementContext>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            // Shows UseCors with CorsPolicyBuilder.
            //app.UseCors(builder =>
            //   builder.WithOrigins("http://localhost:4201")
            //   .AllowAnyHeader()
            //   .AllowAnyMethod());

            app.UseStaticFiles();

            app.UseCors(builder =>
               builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod());

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<PManagementContext>();
                //context.Database.EnsureCreated();
                context.Database.Migrate();
            }

            app.ApplyAuthorizationValidation();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
