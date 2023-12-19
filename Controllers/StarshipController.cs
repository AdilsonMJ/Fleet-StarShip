using FleetCommandAPI.Data;
using FleetCommandAPI.Integration.Interface;
using FleetCommandAPI.Integration.Response.Refit;
using FleetCommandAPI.Model;
using FleetCommandAPI.Model.DTO;
using FleetCommandAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
using FleetCommandAPI.Core.Entity.Maps.Interface;
using Microsoft.AspNetCore.Authorization;
using FleetCommandAPI.Core.Repository;



namespace FleetCommandAPI.Controllers
{
    [Route("[controller]")]
    public class StarshipController : Controller
    {

        private readonly IStarshipIntegration _startshipIntegration;
        private readonly IPlanetIntegration _planetIntegration;
        private readonly FleetStarShipsContext _starshipContext;
        private readonly IStarshipMap _starshipMap;

        private readonly IStarshipRepository _starshipRepository;


        public StarshipController(
            IStarshipIntegration starshipIntegration,
            FleetStarShipsContext fleetStarShipsContext,
            IPlanetIntegration planetIntegration,
            IStarshipMap starshipMap, 
            IStarshipRepository starshipRepository
           )
        {
            _startshipIntegration = starshipIntegration;
            _starshipContext = fleetStarShipsContext;
            _planetIntegration = planetIntegration;
            _starshipMap = starshipMap;
            _starshipRepository = starshipRepository;
        }


        [HttpGet]
        public async Task<ActionResult<List<StarshipsResponse>>> getAllStarships()
        {

            var startShips = await _starshipRepository.GetAllStarshipsWithMissions();

            return Ok (startShips);

        }

        [HttpPost]
        public async Task<ActionResult> AddStarShip([FromBody] StarshipDTO starshipDTO)
        {
            starshipDTO.id = new Random().Next(100, int.MaxValue);

            StarshipModel starShipModel = _starshipMap.StarshipDtoToStarshipModel(starshipDTO);
            await _starshipContext.AddAsync(starShipModel);
            await _starshipContext.SaveChangesAsync();
            return CreatedAtAction(nameof(getById), new { id = starShipModel.id }, starShipModel);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> getById(int id)
        {
            var starShip = await _starshipContext.ships.Include(s => s.missionsModels).FirstOrDefaultAsync(c => c.id == id);
            if (starShip == null) return NotFound();
            var starshipRead = _starshipMap.StarshipModelToStarshipReadDto(starShip);
            return Ok(starshipRead);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpDateStarShipPatch(int id, [FromBody] JsonPatchDocument<StarshipDTO> patch)
        {

            var starShip = await _starshipContext.ships.FirstOrDefaultAsync(c => c.id == id);
            if (starShip == null) return NotFound();


            StarshipDTO starshipDTO = _starshipMap.starshipModelToStarshipDto(starShip);
            patch.ApplyTo(starshipDTO, ModelState);
            if (!TryValidateModel(starshipDTO)) return ValidationProblem(ModelState);

            starShip.id = starshipDTO.id;
            starShip.name = starshipDTO.name;
            starShip.manufacturer = starshipDTO.manufacturer;
            starShip.model = starshipDTO.model;

            await _starshipContext.SaveChangesAsync();
            return NoContent();
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpDatestarship(int id, [FromBody] StarshipDTO starshipDto)
        {
            var starship = await _starshipContext.ships.FirstOrDefaultAsync(c => c.id == id);
            if (starship == null) return NotFound();


            starship.id = id;
            starship.name = starshipDto.name;
            starship.model = starshipDto.model;
            starship.manufacturer = starshipDto.manufacturer;

            await _starshipContext.SaveChangesAsync();
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



        [HttpGet("import-data")]
        [Authorize(policy: "Adm-Master")]
        public async Task<ActionResult> importData()
        {

            var response = await _startshipIntegration.getAllStarships();
            if (response == null)
            {
                return BadRequest();
            }

            var startShipSave = _starshipMap.starshipResponseToStarshipModel(response);

            await _starshipContext.ships.AddRangeAsync(startShipSave);
            await _starshipContext.SaveChangesAsync();

            return NoContent();
        }
    }
}