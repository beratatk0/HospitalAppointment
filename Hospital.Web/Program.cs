using Autofac;
using Autofac.Extensions.DependencyInjection;
using Hospital.Core.Models;
using Hospital.Repo;
using Hospital.Service.Mapper;
using Hospital.Web.Modules;
using Hospital.Web.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace Hospital.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), options =>
            {
                options.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);

            }));

            builder.Services.AddScoped(typeof(NotFoundFilter<,>));
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new RepoServiceModule()));

            builder.Services.AddAutoMapper(typeof(MapProfile));


            // For Identity  
            builder.Services.AddIdentity<PatientIdentity, IdentityRole>()
                            .AddEntityFrameworkStores<AppDbContext>();
            // Adding Authentication  
            builder.Services.AddAuthentication();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Patient/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Authentication}/{action=Login}/{id?}");

            app.Run();
        }
    }
}