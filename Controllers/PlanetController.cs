using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FleetCommandAPI.Integration.Interface;
using FleetCommandAPI.Model.DTO.Planet;
using FleetCommandAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FleetCommandAPI.Controllers
{
    [Route("[controller]")]
    public class PlanetController : Controller
    {
        private readonly IPlanetIntegration _planetIntegration;
        public PlanetController(IPlanetIntegration planetIntegration)
        {
            _planetIntegration = planetIntegration;
        }



        [HttpGet]
        public async Task<ActionResult> getAllPlanet()
        {

            var response = await _planetIntegration.getAllPlanet();

            if (response == null) return BadRequest();

            var planetDTO = response.Select(p => new PlanetReadDTO
            {
                id = ExtractID.FromUrl(p.Url),
                Name = p.Name,
                Population = p.Population,
                Terrain = p.Terrain,
                Url = p.Url
            }).ToList();

            return Ok(planetDTO);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult> getById(string id)
        {
            var response = await _planetIntegration.getPlanetById(id);
            if (response == null ) return NotFound();

            return Ok(response);
        }

    }

}