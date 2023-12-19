using FleetCommandAPI.Model.DTO;
using Microsoft.AspNetCore.JsonPatch;

namespace FleetCommandAPI.Core.Repository
{
    public interface IStarshipRepository
    {
        Task<IEnumerable<StarshipReadWithMissions>> GetAllStarshipsWithMissions();

        Task<StarshipReadWithMissions?> GetByID(int id);

        Task<int?> Save(StarshipDTO starshipDTO);

        Task<bool> Update(int id, StarshipDTO starshipDTO);

        Task<bool> UpdateByPatch(int id, JsonPatchDocument<StarshipDTO> patch);

        Task<bool> Delete(int id);
    }
}