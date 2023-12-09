
using FleetCommandAPI.Data;
using FleetCommandAPI.Model;
using FleetCommandAPI.Model.DTO;
using FleetCommandAPI.Model.DTO.Planet;
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

            if(planet == null) return NotFound();

            MissionsModel missions = new MissionsModel
            {
                Title = missaionDto.Title,
                Planet = planet,
                Goal = missaionDto.Goal,
            };

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
            var missaionDto = missions.Select(m => new MissionReadDTO
            {
                Id = m.Id,
                Title = m.Title,
                Planet = new PlanetReadDTO {
                    id = m.Planet.id,
                    Name = m.Planet.Name,
                    Url = Url.Action("getById", "Planet", new { id = m.Planet.id }, Request.Scheme)
                },
                Goal = m.Goal,
                starships = m.starships.Select(s => new StarshipReadDTO
                {
                    id = s.id,
                    name = s.name,
                    model = s.model,
                    manufacturer = s.manufacturer,
                    link = Url.Action("getById", "Starship", new { id = s.id }, Request.Scheme)

                }).ToList()
            }).ToList();


            return Ok(missaionDto);



        }

        [HttpGet("{id}")]
        public async Task<ActionResult> getById(int id)
        {
            var mission = await _fleetStarShipsContext.missions.Include(s => s.starships).Include(p => p.Planet).FirstOrDefaultAsync(e => e.Id == id);
            if(mission == null) return BadRequest();


            var missionDto = new MissionReadDTO
            {
                Id = mission.Id,
                Title = mission.Title,
                Planet = new PlanetReadDTO {
                    id = mission.Planet.id,
                    Name = mission.Planet.Name,
                    Url = Url.Action("getById", "Planet", new { id = mission.Planet.id }, Request.Scheme)
                },
                Goal = mission.Goal,
                starships = mission.starships.Select(s => new StarshipReadDTO
                {
                    id = s.id,
                    name = s.name,
                    model = s.model,
                    manufacturer = s.manufacturer,
                    link = Url.Action("getById", "Starship", new { id = s.id }, Request.Scheme)

                }).ToList()
            };


            return Ok(missionDto);
        
        }
    }
}