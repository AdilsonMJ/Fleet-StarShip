using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FleetCommandAPI.Core.Entity.Maps.Interface;
using FleetCommandAPI.Data;
using FleetCommandAPI.Model.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace FleetCommandAPI.Core.Repository
{
    public class StarshipRepositoryImpl : IStarshipRepository
    {

        private readonly FleetStarShipsContext _DbContext;
        private readonly IStarshipMap _starshipMap;

        public StarshipRepositoryImpl(FleetStarShipsContext fleetStarShipsContext, IStarshipMap starshipMap)
        {
            _DbContext = fleetStarShipsContext;
            _starshipMap = starshipMap;
        }

        public async Task<IEnumerable<StarshipReadWithMissions>> GetAllStarshipsWithMissions()
        {

                var existStarShips = await _DbContext.ships.Include(m => m.missionsModels).ToListAsync();
                var starshipReadDto = _starshipMap.starshipModelToStarshipReadWithMissions(existStarShips);
                return starshipReadDto;

        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}