using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using obg.BusinessLogic.Interface.Interfaces;
using obg.BusinessLogic.Logics;
using obg.DataAccess.Interface.Interfaces;
using obg.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using obg.DataAccess.Repositories;
using obg.BusinessLogic.Interface;
using obg.WebApi.Filters;

namespace obg.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connection = @"Server=.\SQLEXPRESS; Database=ObgDA2; Integrated Security=True; Trusted_Connection=True; MultipleActiveResultSets=True";
            services.AddDbContext<ObgContext>(options => options.UseSqlServer(connection));
            services.AddControllers();
            services.AddScoped<IPharmacyService, PharmacyService>();
            services.AddScoped<IPharmacyManagement, PharmacyManagement>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserManagement, UserManagement>();
            services.AddScoped<IAdministratorManagement, AdministratorManagement>();
            services.AddScoped<IOwnerManagement, OwnerManagement>();
            services.AddScoped<IEmployeeManagement, EmployeeManagement>();
            services.AddScoped<IMedicineService, MedicineService>();
            services.AddScoped<IMedicineManagement, MedicineManagement>();
            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<IPurchaseManagement, PurchaseManagement>();
            services.AddScoped<IDemandService, DemandService>();
            services.AddScoped<IDemandManagement, DemandManagement>();
            services.AddScoped<IInvitationService, InvitationService>();
            services.AddScoped<IInvitationManagement, InvitationManagement>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<ISessionManagement, SessionManagement>();
            services.AddScoped<IExporterService, ExporterService>();
            services.AddScoped<IPurchaseLineService, PurchaseLineService>();
            services.AddScoped<IPurchaseLineManagement, PurchaseLineManagement>();
            services.AddScoped<AdministratorAuthorizationAttributeFilter>();
            services.AddScoped<OwnerAuthorizationAttributeFilter>();
            services.AddScoped<EmployeeAuthorizationAttributeFilter>();
            services.AddScoped<ExceptionFilter>();

            services.AddCors(c =>
            {
                c.AddPolicy("CorsPolicy", options =>
                options.WithOrigins("http://localhost:4200").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ObgContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            db.Database.EnsureCreated();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
