
using FleetCommandAPI.Core.Entity.Maps;
using FleetCommandAPI.Core.Model.Maps;
using FleetCommandAPI.Data;
using FleetCommandAPI.Model;
using FleetCommandAPI.Model.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace FleetCommandAPI.Controllers
{
    [Route("[controller]")]
    public class MissionsController : Controller
    {

        private readonly FleetStarShipsContext _fleetStarShipsContext;
        private readonly IMissionsMap _missionsMap;

        public MissionsController(FleetStarShipsContext fleetStarShipsContext, IMissionsMap missionsMap)
        {
            _fleetStarShipsContext = fleetStarShipsContext;
            _missionsMap = missionsMap;


        }

        [HttpPost]
        public async Task<IActionResult> SaveMissions([FromBody] MissionDto missaionDto)
        {

            var planet = await _fleetStarShipsContext.planet.FirstOrDefaultAsync(p => p.id == missaionDto.Planet.id);

            if (planet == null) return NotFound();

            // MissionsModel missions = MissionsMaps.missionDtoToMissionModel(missaionDto, planet);
            // foreach (int shipId in missaionDto.starshipsId)
            // {
            //     var ship = await _fleetStarShipsContext.ships.FindAsync(shipId);
            //     if (ship != null)
            //     {
            //         missions.starships.Add(ship);
            //     }
            //     else
            //     {
            //         return BadRequest($"The start Ship #{shipId} not exist!");
            //     }
            // }

            MissionsModel missions = _missionsMap.missionDtoToMissionModel(missaionDto, planet);

            await _fleetStarShipsContext.missions.AddAsync(missions);
            await _fleetStarShipsContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<MissionReadDTO>>> getAllMissions()
        {
            var missions = await _fleetStarShipsContext.missions.Include(c => c.starships).Include(p => p.Planet).ToListAsync();

            var missaionReadDTO = _missionsMap.missionModelToMissionReadDTO(missions);

            return Ok(missaionReadDTO);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult> getById(int id)
        {
            var mission = await _fleetStarShipsContext.missions.Include(s => s.starships).Include(p => p.Planet).FirstOrDefaultAsync(e => e.Id == id);
            if (mission == null) return BadRequest();

            var missionReadDTO = _missionsMap.missionModelToMissionReadDTO(mission);

            return Ok(missionReadDTO);

        }
    }
}