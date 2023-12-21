using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FleetCommandAPI.Core.Entity.Maps;
using FleetCommandAPI.Data;
using FleetCommandAPI.Model.DTO.Planet;
using Microsoft.EntityFrameworkCore;

namespace FleetCommandAPI.Core.Repository.Planet
{
    public class PlanetRepositoryImpl : IPlanetRepository
    {

        private readonly IPlanetMaps _planetMaps;
        private readonly FleetStarShipsContext _dbContext;

        public PlanetRepositoryImpl(IPlanetMaps planetMaps, FleetStarShipsContext fleetStarShipsContext)
        {
            _dbContext = fleetStarShipsContext;
            _planetMaps = planetMaps;
        }


        public async Task<List<PlanetReadDTO>> GetAllPlanets()
        {
           var response = await _dbContext.planet.Include(c => c.missions).ToListAsync();
           var planets = _planetMaps.planetModelToPlanetReadDTO(response);
           return planets;
        }

        public async Task<PlanetReadDTO> GetById(int id)
        {
            var response = await _dbContext.planet.Include(m => m.missions).FirstOrDefaultAsync(p => p.id == id);
            if (response == null) return null;

            return _planetMaps.planetModelToPlanetReadDto(response);
        }
    }
}