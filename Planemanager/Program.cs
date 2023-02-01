using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Planemanager.Data;
using PM.Data;
using PM.Data.Entity;
using PM.Buisness.Repositories;
using PM.Buisness.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using PM.Buisness.Mapper;

namespace Planemanager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSingleton<WeatherForecastService>();


            builder.Services.AddAutoMapper(typeof(MappingConfig));
            builder.Services.AddDbContext<ApplicationDbContext>(option =>
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IPlaneRepositorie, PlaneRepository>();
            builder.Services.AddScoped<IRepository<PM.Data.Entity.Plane>, Repository<PM.Data.Entity.Plane>>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();


        }
    }
}