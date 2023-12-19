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
        IEnumerable<StarshipReadWithMissions> starshipModelToStarshipReadWithMissions(List<StarshipModel> starshipModels);
        StarshipReadWithMissions starshipModelToStarshipReadWithMissions(StarshipModel starshipModel);

        List<StarshipModel> starshipResponseToStarshipModel (List<StarshipsResponse> starshipsResponses);

        StarshipModel StarshipDtoToStarshipModel(StarshipDTO starshipDTO);

        StarshipReadDTO StarshipModelToStarshipReadDto (StarshipModel starshipModel);

        StarshipDTO starshipModelToStarshipDto (StarshipModel starshipModel);
    }
}