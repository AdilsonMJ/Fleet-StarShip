using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FleetCommandAPI.Integration.Response;
using FleetCommandAPI.Model;
using FleetCommandAPI.Model.DTO;
using FleetCommandAPI.Model.DTO.Planet;
using FleetCommandAPI.Utils;
using Microsoft.AspNetCore.Mvc;

namespace FleetCommandAPI.Core.Entity.Maps
{
    public class PlanetMaps : IPlanetMaps
    {

        private readonly ILinkService _linkService;


        public PlanetMaps(ILinkService linkService)
        {
            _linkService = linkService;
        }


        public List<PlanetReadDTO> planetModelToPlanetReadDTO(List<PlanetModel> planetModels)
        {
            var planet = planetModels.Select(p => new PlanetReadDTO
                {
                    id = p.id,
                    Name = p.Name,
                    Population = p.Population,
                    Terrain = p.Terrain,
                    missions = p.missions.Select(m => new MissionReadDTOWithoutList
                    {
                        Id = m.Id,
                        Title = m.Title,
                        Goal = m.Goal,
                        Url = _linkService.GenerateMissionsLink(m.Id)

                    }).ToList(),
                    Url = p.Url
                }).ToList();


            return planet;
        }

        public PlanetReadDTO planetModelToPlanetReadDto(PlanetModel planetModel)
        {
            var planet = new PlanetReadDTO
            {
                id = planetModel.id,
                Name = planetModel.Name,
                Population = planetModel.Population,
                Terrain = planetModel.Terrain,
                Url = planetModel.Url,
                missions = planetModel.missions.Select(m => new MissionReadDTOWithoutList
                {
                    Id = m.Id,
                    Title = m.Title,
                    Goal = m.Goal,
                    Url = _linkService.GenerateMissionsLink(m.Id)
                }).ToList()

            };


            return planet;
        }

        public List<PlanetModel> planetResponseToPlanetModel(List<PlanetResponse> planetResponses)
        {
            var planetModels = planetResponses.Select(p => new PlanetModel
            {
                id = ExtractID.FromUrl(p.Url),
                Name = p.Name,
                Population = p.Population,
                Terrain = p.Terrain,
                Url = p.Url
            }).ToList();

            return planetModels;

        }

        
    }
}