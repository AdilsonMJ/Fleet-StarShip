using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using FleetCommandAPI.Integration.Response.Refit;

namespace FleetCommandAPI.Integration.Interface
{
    public interface IStarshipIntegration
    {
        Task<List<StarshipsResponse>> getAllStarships();
        Task<StarshipsResponse> getStarshipById(string name);
    }
}