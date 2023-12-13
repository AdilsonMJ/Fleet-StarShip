using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleetCommandAPI.Utils
{
    public interface ILinkService
    {
        string GeneratePlanetLink(int planetId);
        string GeneratesStarshipLink(int startshipId);

        string GenerateMissionsLink(int missionId);
    }
}