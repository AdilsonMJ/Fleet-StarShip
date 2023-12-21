using FleetCommandAPI.Core.Entity.Maps;
using FleetCommandAPI.Core.Entity.Maps.Interface;
using FleetCommandAPI.Data;
using FleetCommandAPI.Integration.Interface;
using Microsoft.EntityFrameworkCore;

namespace FleetCommandAPI.Core.Repository.ImportData
{
    public class ImportDataRepositoryImpl : IImportDataRepository
    {

        private readonly IPlanetIntegration _planetIntegration;
        private readonly IStarshipIntegration _startshipIntegration;
        private readonly FleetStarShipsContext _dbContext;
        private readonly IStarshipMap _starshipMap;
        private readonly IPlanetMaps _planetMaps;

        public ImportDataRepositoryImpl(
            IPlanetIntegration planetIntegration,
            IStarshipIntegration startshipIntegration,
            FleetStarShipsContext fleetStarShipsContext,
            IStarshipMap starshipMap,
            IPlanetMaps planetMaps)
        {
            _planetIntegration = planetIntegration;
            _startshipIntegration = startshipIntegration;
            _dbContext = fleetStarShipsContext;
            _starshipMap = starshipMap;
            _planetMaps = planetMaps;
        }


        public async Task<bool> GetStarshipsResponses()
        {
            var response = _startshipIntegration.getAllStarships().Result;

            if (response == null) return false;

            var startShipSave = _starshipMap.starshipResponseToStarshipModel(response);

            foreach (var s in startShipSave)
            {
                var startshipExist = await _dbContext.ships.FirstOrDefaultAsync(start => start.id == s.id);
                if (startshipExist == null)
                {
                    await _dbContext.AddAsync(s);
                    await _dbContext.SaveChangesAsync();
                }
            }
            return true;
        }

        public async Task<bool> GetPlanetsResponse()
        {
            var response = _planetIntegration.getAllPlanet().Result;
            if (response == null) return false;

            var planetsModel = _planetMaps.planetResponseToPlanetModel(response);

            foreach (var p in planetsModel)
            {
                var existPlanet = _dbContext.planet.FirstOrDefaultAsync(planet => planet.id == p.id);

                if (existPlanet == null)
                {
                    await _dbContext.AddAsync(p);
                    await _dbContext.SaveChangesAsync();
                }
            }
            return true;

        }



    }
}