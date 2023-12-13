
using Microsoft.AspNetCore.Mvc;

namespace FleetCommandAPI.Utils
{
    public class LinkService : ILinkService
    {

        private readonly IUrlHelper _urlHelper;


        public LinkService(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        public string GenerateMissionsLink(int missionId)
        {
            return _urlHelper.ActionLink("getById", "Missions", new { id = missionId });
        }

        public string GeneratePlanetLink(int planetId)
        {
            return _urlHelper.ActionLink("getById", "Planet", new { id = planetId });
        }

        public string GeneratesStarshipLink(int startshipId)
        {
            return _urlHelper.ActionLink("getById", "Starship", new { id = startshipId });
        }
    }
}