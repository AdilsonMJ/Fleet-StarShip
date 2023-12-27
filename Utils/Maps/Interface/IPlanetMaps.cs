using FleetCommandAPI.Integration.Response;
using FleetCommandAPI.Model;
using FleetCommandAPI.Model.DTO.Planet;

namespace FleetCommandAPI.Core.Entity.Maps
{
    public interface IPlanetMaps
    {
        List<PlanetReadDTO> planetModelToPlanetReadDTO(List<PlanetModel> planetModels);
        List<PlanetModel> planetResponseToPlanetModel(List<PlanetResponse> planetResponses);
        PlanetReadDTO planetModelToPlanetReadDto(PlanetModel planetModel);
    }
}