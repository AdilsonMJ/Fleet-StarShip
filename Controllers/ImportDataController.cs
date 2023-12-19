using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FleetCommandAPI.Core.Repository.ImportData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FleetCommandAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImportDataController : ControllerBase
    {
        
        private readonly IRepositoryImporData _repositoryImporData;

        public ImportDataController(IRepositoryImporData repositoryImporData)
        {
            _repositoryImporData = repositoryImporData;
        }

        [HttpGet("/Starships")]
        [Authorize(policy: "Adm-Master")]
        public async Task<ActionResult> importData()
        {

            bool response = _repositoryImporData.GetStarshipsResponses().Result;

            if(response is false) return BadRequest();

            return NoContent();
        }

    }
}