
using Refit;

namespace FleetCommandAPI.Integration.Response.Refit
{
    public interface IAPIExternIntragration
    {
        [Get("/api/starships?format=json")]
        Task<ApiResponse<ResultStarship>> getAllStarships();

        [Get("/api/starships/{id}/?format=json")]
        Task<ApiResponse<StarshipsResponse>> getById(string id);

        [Get("/api/planets?format=json")]
        Task<ApiResponse<ResultPlanet>> getAllPlanet();

        [Get("/api/planets/{id}?format=json")]
        Task<ApiResponse<PlanetResponse>> getPlanetById(int id);

    }
}