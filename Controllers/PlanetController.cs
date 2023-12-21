
using FleetCommandAPI.Core.Repository.Planet;
using Microsoft.AspNetCore.Mvc;

namespace FleetCommandAPI.Controllers
{
    [Route("[controller]")]
    public class PlanetController : Controller
    {

        private readonly IPlanetRepository _planetRepository;
        public PlanetController( IPlanetRepository planetRepository)
        {
            _planetRepository = planetRepository;
        }

        [HttpGet]
        public async Task<ActionResult> getAllPlanet()
        {
            var planets = _planetRepository.GetAllPlanets().Result;
            return Ok(planets);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult> getById(int id)
        {
            var planet = _planetRepository.GetById(id).Result;
            if (planet == null) return NotFound();
            return Ok(planet);
        }

    }

}