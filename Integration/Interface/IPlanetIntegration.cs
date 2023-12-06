using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleetCommandAPI.Integration.Interface
{
    public interface IPlanetIntegration
    {
        Task<List<Response.PlanetResponse>> getAllPlanet();
        Task<Response.PlanetResponse> getPlanetById(string id);
    }
}