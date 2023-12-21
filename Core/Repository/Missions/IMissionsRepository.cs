using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FleetCommandAPI.Model;
using FleetCommandAPI.Model.DTO;

namespace FleetCommandAPI.Core.Repository.Missions
{
    public interface IMissionsRepository
    {
        Task<List<MissionReadDTOWithStarships>> GetAllMissions();

        Task<MissionReadDTOWithStarships?> GetById(int id);

        Task<bool> Save(MissionDto missionDto);
    }
}