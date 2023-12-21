using FleetCommandAPI.Core.Entity.Maps;
using FleetCommandAPI.Data;
using FleetCommandAPI.Model;
using FleetCommandAPI.Model.DTO;
using Microsoft.EntityFrameworkCore;

namespace FleetCommandAPI.Core.Repository.Missions
{
    public class MissionsRepositoryImpl : IMissionsRepository
    {

        private readonly FleetStarShipsContext _dbContext;
        private readonly IMissionsMap _missionsMap;

        public MissionsRepositoryImpl(FleetStarShipsContext fleetStarShipsContext, IMissionsMap missionsMap)
        {
            _dbContext = fleetStarShipsContext;
            _missionsMap = missionsMap;

        }


        public async Task<List<MissionReadDTOWithStarships>> GetAllMissions()
        {
            var result = await _dbContext.missions.Include(s => s.starships).Include(p => p.Planet).ToListAsync();
            var missionsDto = _missionsMap.missionModelToMissionReadDTOWithStarships(result);

            return missionsDto;

        }

        public async Task<MissionReadDTOWithStarships?> GetById(int id)
        {
            var mission = await _dbContext.missions.Include(s => s.starships).Include(p => p.Planet).FirstOrDefaultAsync(m => m.Id == id);
            if (mission == null) return null;

            var result = _missionsMap.missionModelToMissionReadDTOWithStarships(mission);

            return result;


        }

        public async Task<bool> Save(MissionDto missionDto)
        {

            var planet = await _dbContext.planet.FirstOrDefaultAsync(p => p.id == missionDto.planetId);
            if (planet == null) throw new ApplicationException("Planet Not Found");

            List<StarshipModel> list = new List<StarshipModel>();

            foreach (var s in missionDto.starshipsId)
            {
                var star = await _dbContext.ships.FirstOrDefaultAsync(m => m.id == s);
                if (star == null)
                {
                    throw new ApplicationException($"Starship  ID #{s} not Found!");
                }
                list.Add(star);
            }

            var mission = _missionsMap.missionDtoToMissionModel(missionDto, planet, list);

            await _dbContext.missions.AddAsync(mission);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}