using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Core;
using FleetCommandAPI.Model;
using FleetCommandAPI.Model.DTO;
using FleetCommandAPI.Model.DTO.Planet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace FleetCommandAPI.Core.Model.Maps
{
    public static class MissionsMaps
    {
        public static MissionsModel missionDtoToMissionModel(MissaionDto missaionDto, PlanetModel planetModel)
        {
            MissionsModel missions = new MissionsModel
            {
                Title = missaionDto.Title,
                Planet = planetModel,
                Goal = missaionDto.Goal,
            };

            return missions;
        }



        public static List<MissionReadDTO> missionModelToMissionReadDTO(List<MissionsModel> missionsModel)
        {
            var missaionReadDTO = missionsModel.Select(m => new MissionReadDTO
            {
                Id = m.Id,
                Title = m.Title,
                Planet = new PlanetReadDTO
                {
                    id = m.Planet.id,
                    Name = m.Planet.Name,
                },
                Goal = m.Goal,
                starships = m.starships.Select(s => new StarshipReadDTO
                {
                    id = s.id,
                    name = s.name,
                    model = s.model,
                    manufacturer = s.manufacturer,
                }).ToList()
            }).ToList();


            return missaionReadDTO;
        }


        public static MissionReadDTO missionModelToMissionReadDTO(MissionsModel mission)
        {

            var missionReadDto = new MissionReadDTO
            {
                Id = mission.Id,
                Title = mission.Title,
                Planet = new PlanetReadDTO
                {
                    id = mission.Planet.id,
                    Name = mission.Planet.Name,
                },
                Goal = mission.Goal,
                starships = mission.starships.Select(s => new StarshipReadDTO
                {
                    id = s.id,
                    name = s.name,
                    model = s.model,
                    manufacturer = s.manufacturer,

                }).ToList()
            };

            return missionReadDto;
        }

    }
}