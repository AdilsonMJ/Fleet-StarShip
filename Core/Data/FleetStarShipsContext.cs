using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FleetCommandAPI.Integration.Response.Refit;
using FleetCommandAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace FleetCommandAPI.Data
{
    public class FleetStarShipsContext : DbContext
    {
        public FleetStarShipsContext(DbContextOptions<FleetStarShipsContext> opts): base(opts){}
        
        public DbSet<StarShipModel> ships { get; set;}

        public DbSet<MissionsModel> missions {get; set;}

        public DbSet<PlanetModel> planet{get; set;}
        
            
    
    }
}