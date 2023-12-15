using FleetCommandAPI.Core.Entity.Maps.Interface;
using FleetCommandAPI.Integration.Response.Refit;
using FleetCommandAPI.Model;
using FleetCommandAPI.Model.DTO;
using FleetCommandAPI.Utils;

namespace FleetCommandAPI.Core.Entity.Maps
{
    public class StarshipMap : IStarshipMap
    {

        private readonly ILinkService _linkService;


        public StarshipMap(ILinkService linkService)
        {
            _linkService = linkService;
        }


        public List<StarshipReadWithouListDto> starshipModelToStarshipReadWithouListDto(List<StarshipModel> starshipModels)
        {

            var starshipRead = starshipModels.Select(e => new StarshipReadWithouListDto
            {
                id = e.id,
                name = e.name,
                model = e.model,
                manufacturer = e.manufacturer,
                missionsModels = e.missionsModels.Select(r => new MissionReadDTOWithoutList
                {
                    Id = r.Id,
                    Title = r.Title,
                    Planet = r.Planet,
                    Goal = r.Goal,
                    Url = _linkService.GenerateMissionsLink(r.Id)
                }).ToList()

            }).ToList();

            return starshipRead;

        }

        public List<StarshipModel> starshipResponseToStarshipModel(List<StarshipsResponse> starshipsResponses)
        {
            var startShips = starshipsResponses.Select(r => new StarshipModel
            {
                id = ExtractID.FromUrl(r.url),
                name = r.name,
                model = r.model,
                manufacturer = r.manufacturer
            }).ToList();

            return startShips;
        }

        public StarshipModel StarshipDtoToStarshipModel(StarshipDTO starshipDTO)
        {
            return new StarshipModel
            {
                id = new Random().Next(100, int.MaxValue),
                name = starshipDTO.name,
                model = starshipDTO.model,
                manufacturer = starshipDTO.manufacturer
            };
        }

        public StarshipReadDTO StarshipModelToStarshipReadDto(StarshipModel starshipModel)
        {
            return new StarshipReadDTO
            {
                id = starshipModel.id,
                name = starshipModel.name,
                model = starshipModel.model,
                manufacturer = starshipModel.manufacturer
            };
        }

        public StarshipDTO starshipModelToStarshipDto(StarshipModel starshipModel)
        {
            return new StarshipDTO
            {
                id = starshipModel.id,
                name = starshipModel.name,
                manufacturer = starshipModel.manufacturer,
                model = starshipModel.model
            };
        }
    }
}