
using Azure;
using FleetCommandAPI.Data;
using FleetCommandAPI.Integration.Interface;
using FleetCommandAPI.Integration.Response.Refit;
using FleetCommandAPI.Model;
using FleetCommandAPI.Model.DTO;
using FleetCommandAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
using FleetCommandAPI.Model.DTO.Planet;


namespace FleetCommandAPI.Controllers
{
    [Route("[controller]")]
    public class FleetController : Controller
    {

        private readonly IStarshipIntegration _startshipIntegration;
        private readonly IPlanetIntegration _planetIntegration;
        private readonly FleetStarShipsContext _starshipContext;


        public FleetController(IStarshipIntegration starshipIntegration, FleetStarShipsContext fleetStarShipsContext, IPlanetIntegration planetIntegration)
        {
            _startshipIntegration = starshipIntegration;
            _starshipContext = fleetStarShipsContext;
            _planetIntegration = planetIntegration;
        }


        [HttpGet]
        public async Task<ActionResult<List<StarshipsResponse>>> getAllStarships()
        {

            var existStarShips = await _starshipContext.ships.Include(c => c.missionsModels).ToListAsync();

            if (existStarShips.Any())
            {

                var starshipRead = existStarShips.Select(e => new StarshipReadToFleetDto
                {
                    id = e.id,
                    name = e.name,
                    model = e.model,
                    manufacturer = e.manufacturer,
                    missionsModels = e.missionsModels.Select(r => new MissionReadToFleetDTO
                    {
                        Id = r.Id,
                        Title = r.Title,
                        Planet = r.Planet,
                        Goal = r.Goal,
                        Link = Url.Action("getById", "Missions", new { id = r.Id }, Request.Scheme)
                    }).ToList()

                }).ToList();

                return Ok(starshipRead);
            }

            var response = await _startshipIntegration.getAllStarships();
            if (response == null)
            {
                return BadRequest();
            }


            var startShipSave = response.Select(r => new StarShipModel
            {
                id = ExtractID.FromUrl(r.url),
                name = r.name,
                model = r.model,
                manufacturer = r.manufacturer
            }).ToList();


            await _starshipContext.ships.AddRangeAsync(startShipSave);
            await _starshipContext.SaveChangesAsync();

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> AddStarShip([FromBody] StarshipDTO starshipDTO)
        {
            starshipDTO.id = new Random().Next(100, int.MaxValue);

            StarShipModel starShipModel = new StarShipModel
            {
                id = new Random().Next(100, int.MaxValue),
                name = starshipDTO.name,
                model = starshipDTO.model,
                manufacturer = starshipDTO.manufacturer
            };

            await _starshipContext.AddAsync(starShipModel);
            await _starshipContext.SaveChangesAsync();



            return CreatedAtAction(nameof(getById), new { id = starShipModel.id }, starShipModel);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> getById(int id)
        {
            var starShip = await _starshipContext.ships.Include(s => s.missionsModels).FirstOrDefaultAsync(c => c.id == id);
            if (starShip == null) return BadRequest();
            return Ok(starShip);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpDateStarShipPatch(int id, JsonPatchDocument<StarshipDTO> patch)
        {

            var starShip = await _starshipContext.ships.FirstOrDefaultAsync(c => c.id == id);
            if (starShip == null) return NotFound();


            StarshipDTO starshipDTO = new StarshipDTO
            {
                id = starShip.id,
                name = starShip.name,
                manufacturer = starShip.manufacturer,
                model = starShip.model
            };

            patch.ApplyTo(starshipDTO, ModelState);
            if (!TryValidateModel(starshipDTO)) return ValidationProblem(ModelState);


            StarShipModel starShipModel = new StarShipModel
            {
                id = starshipDTO.id,
                name = starshipDTO.name,
                manufacturer = starshipDTO.manufacturer,
                model = starshipDTO.model
            };

            await _starshipContext.SaveChangesAsync();
            return NoContent();
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpDatestarship(int id, [FromBody] StarshipDTO starshipDto)
        {
            var starship = _starshipContext.ships.FirstOrDefaultAsync(c => c.id == id);
            if (starship == null) return NotFound();

            StarShipModel starShipModel = new StarShipModel
            {
                id = id,
                name = starshipDto.name,
                model = starshipDto.model,
                manufacturer = starshipDto.manufacturer
            };

            await _starshipContext.AddRangeAsync();

            return NoContent();

        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var starship = await _starshipContext.ships.FindAsync(id);
            if (starship == null) return NotFound();

            _starshipContext.ships.Remove(starship);
            await _starshipContext.SaveChangesAsync();

            return NoContent();

        }

    }
}