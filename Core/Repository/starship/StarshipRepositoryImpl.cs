
using FleetCommandAPI.Core.Entity.Maps.Interface;
using FleetCommandAPI.Data;
using FleetCommandAPI.Model;
using FleetCommandAPI.Model.DTO;
using Microsoft.AspNetCore.JsonPatch;
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

        public async Task<StarshipReadWithMissions?> GetByID(int id)
        {
            var result = await _DbContext.ships.Include(m => m.missionsModels).FirstOrDefaultAsync(s => s.id == id);

            if (result == null) return null;

            var starshipRead = _starshipMap.starshipModelToStarshipReadWithMissions(result);

            return starshipRead;

        }


        public async Task<int?> Save(StarshipDTO starshipDTO)
        {

            var exist = await _DbContext.ships.FirstOrDefaultAsync(s => s.name == starshipDTO.name);

            if (exist.model == starshipDTO.model) return null;


            starshipDTO.id = new Random().Next(100, int.MaxValue);
            StarshipModel starshipModel = _starshipMap.StarshipDtoToStarshipModel(starshipDTO);

            await _DbContext.AddAsync(starshipModel);
            await _DbContext.SaveChangesAsync();

            return starshipModel.id;
        }


        public async Task<bool> Delete(int id)
        {
            var starship = await _DbContext.ships.FirstOrDefaultAsync(m => m.id == id);
            if (starship == null) return false;

            _DbContext.ships.Remove(starship);
            await _DbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Update(int id, StarshipDTO starshipDTO)
        {
            var starship = await _DbContext.ships.FirstOrDefaultAsync(c => c.id == id);
            if (starship == null) return false;

            if (starship.name != starshipDTO.name || starship.model != starshipDTO.model)
            {
                var exist = await _DbContext.ships.FirstOrDefaultAsync(s => s.name == starshipDTO.name && s.model == starshipDTO.model);
                if (exist != null) return false;
            }

            starship.id = id;
            starship.name = starshipDTO.name;
            starship.model = starshipDTO.model;
            starship.manufacturer = starshipDTO.manufacturer;

            await _DbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateByPatch(int id, JsonPatchDocument<StarshipDTO> patch)
        {
            var starShipModel = await _DbContext.ships.FirstOrDefaultAsync(s => s.id == id);
            if (starShipModel == null) return false;

            StarshipDTO starship = _starshipMap.starshipModelToStarshipDto(starShipModel);
            patch.ApplyTo(starship);

            if (starship.name != starShipModel.name || starship.model != starShipModel.model)
            {
                var exist = await _DbContext.ships.FirstOrDefaultAsync(s => s.name == starship.name && s.model == starship.model);

                if (exist != null) return false;
            }

            starShipModel.id = starship.id;
            starShipModel.name = starship.name;
            starShipModel.manufacturer = starship.manufacturer;
            starShipModel.model = starship.model;

            await _DbContext.SaveChangesAsync();

            return true;
        }
    }
}