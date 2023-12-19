using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FleetCommandAPI.Core.Entity.Maps;
using FleetCommandAPI.Data;
using FleetCommandAPI.Integration;
using FleetCommandAPI.Integration.Integration;
using FleetCommandAPI.Integration.Response.Refit;

namespace FleetCommandAPI.Core.Repository.ImportData
{
    public class ImportDataRepositoryImpl : IRepositoryImporData
    {

        private readonly PlanetIntegration _planetIntegration;
        private readonly StartshipIntegration _startshipIntegration;
        private readonly FleetStarShipsContext _dbContext;

        private readonly StarshipMap _starshipMap;

        public ImportDataRepositoryImpl(PlanetIntegration planetIntegration, StartshipIntegration startshipIntegration, FleetStarShipsContext fleetStarShipsContext, StarshipMap starshipMap)
        {
            _planetIntegration = planetIntegration;
            _startshipIntegration = startshipIntegration;
            _dbContext = fleetStarShipsContext;
            _starshipMap = starshipMap;
        }


        // I have to validate whether the data does not exist in the database.
        public async Task<bool> GetStarshipsResponses()
        {
            var response =  _startshipIntegration.getAllStarships().Result;

            if (response == null) return false;
            
            var startShipSave = _starshipMap.starshipResponseToStarshipModel(response);

            await _dbContext.ships.AddRangeAsync(startShipSave);
            await _dbContext.SaveChangesAsync();

            return true;

        }
    }
}