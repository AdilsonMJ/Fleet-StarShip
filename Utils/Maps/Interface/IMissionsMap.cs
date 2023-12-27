using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FleetCommandAPI.Model;
using FleetCommandAPI.Model.DTO;

namespace FleetCommandAPI.Core.Entity.Maps
{
    public interface IMissionsMap
    {
        MissionsModel missionDtoToMissionModel(MissionDto missaionDto, PlanetModel planetModel, List<StarshipModel> starshipModels);
        List<MissionReadDTOWithStarships> missionModelToMissionReadDTOWithStarships(List<MissionsModel> missionsModel);
        public MissionReadDTOWithStarships missionModelToMissionReadDTOWithStarships(MissionsModel mission);
    }
}