
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

            MissionsModel missions = new MissionsModel
            {
                Title = missaionDto.Title,
                Planet = missaionDto.Planet,
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
            var missions = await _fleetStarShipsContext.missions.Include(c => c.starships).ToListAsync();

            var missaionDto = missions.Select(m => new MissionReadDTO
            {
                Id = m.Id,
                Title = m.Title,
                Planet = m.Planet,
                Goal = m.Goal,
                starships = m.starships.Select(s => new StarshipReadDTO
                {

                    id = s.id,
                    name = s.name,
                    model = s.model,
                    manufacturer = s.manufacturer,
                    link = Url.Action("getById", "Fleet", new { id = s.id }, Request.Scheme)

                }).ToList()
            }).ToList();


            return Ok(missaionDto);



        }

        [HttpGet("{id}")]
        public async Task<ActionResult> getById(int id)
        {
            var mission = await _fleetStarShipsContext.missions.FirstOrDefaultAsync(e => e.Id == id);
            if(mission == null) return BadRequest();

            return Ok(mission);
        
        }
    }
}