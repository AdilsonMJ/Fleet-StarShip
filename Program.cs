
using FleetCommandAPI.Data;
using FleetCommandAPI.Integration;
using FleetCommandAPI.Integration.Interface;
using Microsoft.EntityFrameworkCore;
using Refit;
using Newtonsoft.Json;
using FleetCommandAPI.Integration.Integration;

namespace FleetCommandAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            var connectionString = builder.Configuration.GetConnectionString("StarShipConnection");
            builder.Services.AddDbContext<FleetStarShipsContext>(opts => opts.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


            builder.Services.AddRefitClient<Integration.Response.Refit.IAPIExternIntragration>().ConfigureHttpClient(c => 
            {
                c.BaseAddress = new Uri("https://swapi.dev/");
            });



            builder.Services.AddScoped<IStarshipIntegration, StartshipIntegration>();
            builder.Services.AddScoped<IPlanetIntegration, PlanetIntegration>();

            builder.Services.AddControllers().AddNewtonsoftJson(opts => {opts.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;});

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}