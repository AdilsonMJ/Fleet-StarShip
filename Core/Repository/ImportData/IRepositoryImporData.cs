using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FleetCommandAPI.Integration.Response.Refit;

namespace FleetCommandAPI.Core.Repository.ImportData
{

    public interface IRepositoryImporData
    {
        Task<bool> GetStarshipsResponses();
    }
}