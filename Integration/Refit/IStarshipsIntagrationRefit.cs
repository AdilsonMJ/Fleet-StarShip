
using Refit;

namespace FleetCommandAPI.Integration.Response.Refit
{
    public interface IStarshipsIntagrationRefit
    {
        [Get("/api/starships?format=json")]
        Task<ApiResponse<Result>> getAllStarships();

        [Get("/api/starships/{name}/?format=json")]
        Task<ApiResponse<StarshipsResponse>> getById(string name);
    }
}