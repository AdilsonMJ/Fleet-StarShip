using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FleetCommandAPI.Model.DTO;

namespace FleetCommandAPI.Core.Repository
{
    public interface IStarshipRepository
    {
        Task<IEnumerable<StarshipReadWithMissions>> GetAllStarshipsWithMissions();

        void Save();

        void Update();

        void Delete();
    }
}