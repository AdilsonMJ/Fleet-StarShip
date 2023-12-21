

using FleetCommandAPI.Core.Repository.Missions;
using FleetCommandAPI.Model.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FleetCommandAPI.Controllers
{
    [Route("[controller]")]
    public class MissionsController : Controller
    {

        private readonly IMissionsRepository _missionsRepository;

        public MissionsController(IMissionsRepository missionsRepository)
        {
            _missionsRepository = missionsRepository;
        }

        [HttpPost]
        public async Task<IActionResult> SaveMissions([FromBody] MissionDto missaionDto)
        {
            bool result = _missionsRepository.Save(missaionDto).Result;
            if (result == false) return BadRequest();
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<MissionReadDTOWithStarships>>> getAllMissions()
        {
            var missions = await _missionsRepository.GetAllMissions();
            return Ok(missions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> getById(int id)
        {
            var mission = _missionsRepository.GetById(id).Result;
            if (mission == null) return NotFound();
            return Ok(mission);

        }
    }
}