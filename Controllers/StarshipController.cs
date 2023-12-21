
using FleetCommandAPI.Integration.Response.Refit;
using FleetCommandAPI.Model.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using FleetCommandAPI.Core.Repository;



namespace FleetCommandAPI.Controllers
{
    [Route("[controller]")]
    public class StarshipController : Controller
    {

        private readonly IStarshipRepository _starshipRepository;


        public StarshipController(IStarshipRepository starshipRepository)
        {
            _starshipRepository = starshipRepository;
        }


        [HttpGet]
        public async Task<ActionResult<List<StarshipsResponse>>> getAllStarships()
        {

            var startShips = await _starshipRepository.GetAllStarshipsWithMissions();

            return Ok(startShips);

        }

        [HttpPost]
        public async Task<ActionResult> AddStarShip([FromBody] StarshipDTO starshipDTO)
        {
            var response = _starshipRepository.Save(starshipDTO).Result;

            if (response is null) return BadRequest("Error: The starship may already exist.");

            return CreatedAtAction(nameof(getById), new { id = response }, starshipDTO);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> getById(int id)
        {
            var result = _starshipRepository.GetByID(id).Result;

            if (result is null) return NotFound();

            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpDateStarShipPatch(int id, [FromBody] JsonPatchDocument<StarshipDTO> patch)
        {

            bool starhip = _starshipRepository.UpdateByPatch(id, patch).Result;

            if(starhip is false) return BadRequest();
            return NoContent();
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpDateStarship(int id, [FromBody] StarshipDTO starshipDto)
        {
            bool starship =  _starshipRepository.Update(id, starshipDto).Result;
            if (starship is false) return NotFound();
            
            return NoContent();

        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool result = _starshipRepository.Delete(id).Result;

            if (result is false) return NotFound();

            return NoContent();

        }

    }
}