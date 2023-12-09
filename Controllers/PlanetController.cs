using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FleetCommandAPI.Data;
using FleetCommandAPI.Integration.Interface;
using FleetCommandAPI.Model;
using FleetCommandAPI.Model.DTO;
using FleetCommandAPI.Model.DTO.Planet;
using FleetCommandAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FleetCommandAPI.Controllers
{
    [Route("[controller]")]
    public class PlanetController : Controller
    {
        private readonly IPlanetIntegration _planetIntegration;
        private readonly FleetStarShipsContext _fleetStarShipsContext;
        public PlanetController(IPlanetIntegration planetIntegration, FleetStarShipsContext fleetStarShipsContext)
        {
            _planetIntegration = planetIntegration;
            _fleetStarShipsContext = fleetStarShipsContext;
        }



        [HttpGet]
        public async Task<ActionResult> getAllPlanet()
        {


            var exist = await _fleetStarShipsContext.planet.Include(c => c.missions).ToListAsync();

            if (exist.Any())
            {
                var planet = exist.Select(p => new PlanetReadDTO
                {
                    id = p.id,
                    Name = p.Name,
                    Population = p.Population,
                    Terrain = p.Terrain,
                    missions = p.missions.Select(m => new MissionReadDTOWithoutList
                    {
                        Id = m.Id,
                        Title = m.Title,
                        Goal = m.Goal,
                        Link = Url.Action("getById", "Missions", new { id = m.Id }, Request.Scheme)

                    }).ToList(),
                    Url = p.Url
                }).ToList();

                return Ok(planet);
            }

            var response = await _planetIntegration.getAllPlanet();
            if (response == null) return BadRequest();

            var planetModels = response.Select(p => new PlanetModel
            {
                id = ExtractID.FromUrl(p.Url),
                Name = p.Name,
                Population = p.Population,
                Terrain = p.Terrain,
                Url = p.Url
            }).ToList();

            await _fleetStarShipsContext.planet.AddRangeAsync(planetModels);
            await _fleetStarShipsContext.SaveChangesAsync();
            return Ok(planetModels);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult> getById(int id)
        {
            var response = await _fleetStarShipsContext.planet.Include(m => m.missions).FirstOrDefaultAsync();
            if (response == null) return NotFound();


            var planet = new PlanetReadDTO
            {
                id = response.id,
                Name = response.Name,
                Population = response.Population,
                Terrain = response.Terrain,
                Url = response.Url,
                missions = response.missions.Select(m => new MissionReadDTOWithoutList
                {
                    Id = m.Id,
                    Title = m.Title,
                    Goal = m.Goal,
                    Link = Url.Action("getById", "Missions", new { id = m.Id }, Request.Scheme)
                }).ToList()

            };


            return Ok(planet);
        }

    }

}