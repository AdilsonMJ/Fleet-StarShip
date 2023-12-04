using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FleetCommandAPI.Data;
using FleetCommandAPI.Model;
using FleetCommandAPI.Model.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Refit;

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
                } else {
                    return BadRequest($"The start Ship #{shipId} not exist!");
                }
            }

            await _fleetStarShipsContext.missions.AddAsync(missions);
            await _fleetStarShipsContext.SaveChangesAsync();

            return Ok();
        }


    }
}