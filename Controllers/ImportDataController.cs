using FleetCommandAPI.Core.Repository.ImportData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FleetCommandAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImportDataController : Controller
    {

        private readonly IImportDataRepository _repositoryImporData;

        public ImportDataController(IImportDataRepository repositoryImporData)
        {
            _repositoryImporData = repositoryImporData;
        }

        [HttpGet("Starships")]
        [Authorize(policy: "Adm-Master")]
        public async Task<ActionResult> importData()
        {

            bool response = _repositoryImporData.GetStarshipsResponses().Result;

            if (response is false) return BadRequest();

            return NoContent();
        }


        [HttpGet("Planets")]
        [Authorize(policy: "Adm-Master")]
        public async Task<ActionResult> importDataPlanets()
        {
            bool response = _repositoryImporData.GetPlanetsResponse().Result;

            if(response is false ) return BadRequest();
            return NoContent();
        }

    }
}