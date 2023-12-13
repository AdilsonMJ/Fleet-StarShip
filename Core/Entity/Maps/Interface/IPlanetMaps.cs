using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FleetCommandAPI.Integration.Response;
using FleetCommandAPI.Model;
using FleetCommandAPI.Model.DTO.Planet;
using Microsoft.AspNetCore.Mvc;

namespace FleetCommandAPI.Core.Entity.Maps
{
    public interface IPlanetMaps
    {
        List<PlanetReadDTO> planetModelToPlanetReadDTO(List<PlanetModel> planetModels);
        List<PlanetModel> planetResponseToPlanetModel(List<PlanetResponse> planetResponses);
        PlanetReadDTO planetModelToPlanetReadDto(PlanetModel planetModel);
    }
}