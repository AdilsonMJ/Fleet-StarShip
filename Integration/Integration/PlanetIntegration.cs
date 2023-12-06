
using FleetCommandAPI.Integration.Interface;
using FleetCommandAPI.Integration.Response;
using FleetCommandAPI.Integration.Response.Refit;

namespace FleetCommandAPI.Integration.Integration
{
    public class PlanetIntegration : IPlanetIntegration
    {
        private readonly IAPIExternIntragration _planetIntegration;

        public PlanetIntegration(IAPIExternIntragration planetIntegrationRefit)
        {
            _planetIntegration = planetIntegrationRefit;
        }

        public async Task<List<PlanetResponse>> getAllPlanet()
        {
            var response = await _planetIntegration.getAllPlanet();

            if (response != null && response.IsSuccessStatusCode)
            {
                return response.Content.results;
            }

            return null;
        }

        public async Task<PlanetResponse> getPlanetById(string id)
        {
            var responde = await _planetIntegration.getPlanetById(id);

            if (responde != null && responde.IsSuccessStatusCode)
            {
                return responde.Content;
            }

            return null;
        }
    }
}