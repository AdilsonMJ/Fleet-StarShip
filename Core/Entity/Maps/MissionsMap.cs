using FleetCommandAPI.Core.Entity.Maps;
using FleetCommandAPI.Model;
using FleetCommandAPI.Model.DTO;
using FleetCommandAPI.Model.DTO.Planet;
using FleetCommandAPI.Utils;


namespace FleetCommandAPI.Core.Model.Maps
{
    public class MissionsMap : IMissionsMap
    {
        

        private readonly ILinkService _linkService;

        public MissionsMap(ILinkService linkService)
        {
            _linkService = linkService;
        }

        public MissionsModel missionDtoToMissionModel(MissionDto missaionDto, PlanetModel planetModel)
        {
            MissionsModel missions = new MissionsModel
            {
                Title = missaionDto.Title,
                Planet = planetModel,
                Goal = missaionDto.Goal,
            };

            return missions;
        }



        public List<MissionReadDTO> missionModelToMissionReadDTO(List<MissionsModel> missionsModel)
        {
            var missionReadDTO = missionsModel.Select(m => new MissionReadDTO
            {
                Id = m.Id,
                Title = m.Title,
                Planet = new PlanetReadDTO
                {
                    id = m.Planet.id,
                    Name = m.Planet.Name,
                    Url = _linkService.GeneratePlanetLink(m.Planet.id)
                },
                Goal = m.Goal,
                starships = m.starships.Select(s => new StarshipReadDTO
                {
                    id = s.id,
                    name = s.name,
                    model = s.model,
                    manufacturer = s.manufacturer,
                    Url = _linkService.GeneratesStarshipLink(s.id)
                }).ToList()
            }).ToList();


            return missionReadDTO;
        }


        public MissionReadDTO missionModelToMissionReadDTO(MissionsModel mission)
        {

            var missionReadDto = new MissionReadDTO
            {
                Id = mission.Id,
                Title = mission.Title,
                Planet = new PlanetReadDTO
                {
                    id = mission.Planet.id,
                    Name = mission.Planet.Name,
                    Url = _linkService.GeneratePlanetLink(mission.Planet.id)
                },
                Goal = mission.Goal,
                starships = mission.starships.Select(s => new StarshipReadDTO
                {
                    id = s.id,
                    name = s.name,
                    model = s.model,
                    manufacturer = s.manufacturer,
                    Url = _linkService.GeneratesStarshipLink(s.id)

                }).ToList()
            };

            return missionReadDto;
        }

    }
}