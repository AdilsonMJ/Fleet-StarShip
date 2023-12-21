using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FleetCommandAPI.Core.Entity.Maps;
using FleetCommandAPI.Data;
using FleetCommandAPI.Model.DTO.Planet;

namespace FleetCommandAPI.Core.Repository.Planet
{
    public interface IPlanetRepository
    {

        Task<List<PlanetReadDTO>> GetAllPlanets();

        Task<PlanetReadDTO> GetById(int id);
    }
}