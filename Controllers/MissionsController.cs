
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

        public MissionsController(FleetStarShipsContext fleetStarShipsContext)
        {
            _fleetStarShipsContext = fleetStarShipsContext;


        }

        [HttpPost]
        public async Task<IActionResult> SaveMissions([FromBody] MissaionDto missaionDto)
        {

            var planet = await _fleetStarShipsContext.planet.FirstOrDefaultAsync(p => p.id == missaionDto.Planet.id);

            if (planet == null) return NotFound();

            MissionsModel missions = MissionsMaps.missionDtoToMissionModel(missaionDto, planet);
            foreach (int shipId in missaionDto.starshipsId)
            {
                var ship = await _fleetStarShipsContext.ships.FindAsync(shipId);
                if (ship != null)
                {
                    missions.starships.Add(ship);
                }
                else
                {
                    return BadRequest($"The start Ship #{shipId} not exist!");
                }
            }


            await _fleetStarShipsContext.missions.AddAsync(missions);
            await _fleetStarShipsContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<MissionsModel>>> getAllMissions()
        {
            var missions = await _fleetStarShipsContext.missions.Include(c => c.starships).Include(p => p.Planet).ToListAsync();

            var missaionReadDTO = MissionsMaps.missionModelToMissionReadDTO(missions);

            foreach (var m in missaionReadDTO)
            {
                m.Planet.Url = Url.Action("getById", "Planet", new { id = m.Planet.id }, Request.Scheme);

                foreach (var s in m.starships)
                {
                    s.link = Url.Action("getById", "Starship", new { id = s.id }, Request.Scheme);
                }
            }

            return Ok(missaionReadDTO);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult> getById(int id)
        {
            var mission = await _fleetStarShipsContext.missions.Include(s => s.starships).Include(p => p.Planet).FirstOrDefaultAsync(e => e.Id == id);
            if (mission == null) return BadRequest();

            var missionReadDTO = MissionsMaps.missionModelToMissionReadDTO(mission);

            mission.Planet.Url = Url.Action("getById", "Planet", new { id = mission.Planet.id });

            foreach (var s in missionReadDTO.starships)
            {
                s.link = Url.Action("getById", "Starship", new { id = s.id }, Request.Scheme);
            }

            return Ok(missionReadDTO);

        }
    }
}