
using FleetCommandAPI.Data;
using FleetCommandAPI.Integration;
using FleetCommandAPI.Integration.Interface;
using Microsoft.EntityFrameworkCore;
using FleetCommandAPI.Integration.Response.Refit;
using Newtonsoft.Json;
using FleetCommandAPI.Integration.Integration;
using FleetCommandAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Refit;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using FleetCommandAPI.Core.Entity.Maps;
using FleetCommandAPI.Core.Model.Maps;
using FleetCommandAPI.Core.Entity.Maps.Interface;
using FleetCommandAPI.Core.Services;
using FleetCommandAPI.Core.Entity.User;
using Microsoft.AspNetCore.Identity;
using FleetCommandAPI.Core.Data;

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
            var connectionStringUser = builder.Configuration.GetConnectionString("UserConnection");
            builder.Services.AddDbContext<FleetStarShipsContext>(opts => opts.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
            builder.Services.AddDbContext<UserDbContext>(opts => opts.UseMySql(connectionStringUser, ServerVersion.AutoDetect(connectionStringUser)));


            builder.Services.AddRefitClient<IAPIExternIntragration>().ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri("https://swapi.dev/");
            });



            builder.Services.AddScoped<IStarshipIntegration, StartshipIntegration>();
            builder.Services.AddScoped<IPlanetIntegration, PlanetIntegration>();
            builder.Services.AddScoped<ILinkService, LinkService>();
            builder.Services.AddScoped<IMissionsMap, MissionsMap>();
            builder.Services.AddScoped<IPlanetMaps, PlanetMaps>();
            builder.Services.AddScoped<IStarshipMap, StarshipMap>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<TokenService>();

            builder.Services.AddIdentity<UserModel, IdentityRole>()
            .AddEntityFrameworkStores<UserDbContext>()
            .AddDefaultTokenProviders();
            
            // To use URLHELPER
            builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            builder.Services.AddScoped<IUrlHelper>(x =>
            {
                var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
                var factory = x.GetRequiredService<IUrlHelperFactory>();
                return factory.GetUrlHelper(actionContext);
            });


            builder.Services.AddControllers().AddNewtonsoftJson(opts => { opts.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; });

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