using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FleetCommandAPI.Integration.Response.Refit;
using FleetCommandAPI.Model;
using FleetCommandAPI.Model.DTO;

namespace FleetCommandAPI.Core.Entity.Maps.Interface
{
    public interface IStarshipMap
    {
        List<StarshipReadWithouListDto> starshipModelToStarshipReadWithouListDto(List<StarshipModel> starshipModels);

        List<StarshipModel> starshipResponseToStarshipModel (List<StarshipsResponse> starshipsResponses);

        StarshipModel StarshipDtoToStarshipModel(StarshipDTO starshipDTO);

        StarshipReadDTO StarshipModelToStarshipReadDto (StarshipModel starshipModel);

        StarshipDTO starshipModelToStarshipDto (StarshipModel starshipModel);
    }
}