using FleetCommandAPI.Core.Entity.Maps;
using FleetCommandAPI.Data;
using FleetCommandAPI.Integration.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FleetCommandAPI.Controllers
{
    [Route("[controller]")]
    public class PlanetController : Controller
    {
        private readonly IPlanetIntegration _planetIntegration;
        private readonly FleetStarShipsContext _fleetStarShipsContext;
        private readonly IPlanetMaps _planetMaps;
        public PlanetController(IPlanetIntegration planetIntegration, FleetStarShipsContext fleetStarShipsContext, IPlanetMaps planetMaps)
        {
            _planetIntegration = planetIntegration;
            _fleetStarShipsContext = fleetStarShipsContext;
            _planetMaps = planetMaps;
        }

        [HttpGet]
        public async Task<ActionResult> getAllPlanet()
        {

            var response = await _fleetStarShipsContext.planet.Include(c => c.missions).ToListAsync();
            var planets = _planetMaps.planetModelToPlanetReadDTO(response);

            return Ok(planets);

        }

        [HttpGet("import-data")]
        [Authorize(policy: "Adm-Master")]
        public async Task<ActionResult> importData()
        {
            var response = await _planetIntegration.getAllPlanet();
            if (response == null) return BadRequest();
            var planetModels = _planetMaps.planetResponseToPlanetModel(response);

            await _fleetStarShipsContext.planet.AddRangeAsync(planetModels);
            await _fleetStarShipsContext.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> getById(int id)
        {
            var response = await _fleetStarShipsContext.planet.Include(m => m.missions).FirstOrDefaultAsync(p => p.id == id);
            if (response == null) return NotFound();
            var planet = _planetMaps.planetModelToPlanetReadDto(response);
            
            return Ok(planet);
        }

    }

}