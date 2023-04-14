
using Microsoft.EntityFrameworkCore;
using PM.API.Endpoints;
using PM.Buisness.Mapper;
using PM.Buisness.Repositories;
using PM.Buisness.Repositories.IRepositories;
using PM.Data;

namespace PM.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //builder.Services.AddAuthorization();
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(typeof(MappingConfig));
            builder.Services.AddDbContext<ApplicationDbContext>(option =>
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IPlaneRepositorie, PlaneRepository>();
            builder.Services.AddCors(options=> options.AddPolicy(name: "PlaneOrigins",
                policy => 
                {
                    policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
                }
                ));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("PlaneOrigins");
            app.UseHttpsRedirection();

            //app.UseAuthorization();
            app.MapControllers();
            //app.ConfigurePlaneEndpoints();

            app.Run();
        }
    }
}