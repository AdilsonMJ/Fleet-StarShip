using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FleetCommandAPI.Integration.Interface;
using FleetCommandAPI.Integration.Response.Refit;

namespace FleetCommandAPI.Integration
{
    public class StartShipIntragration : IStarshipIntegration
    {

        private readonly IStarshipsIntagrationRefit _starshipsIntagrationRefit;

        public StartShipIntragration(IStarshipsIntagrationRefit starshipsIntagrationRefit)
        {
            _starshipsIntagrationRefit = starshipsIntagrationRefit;
        }

        public async Task<List<StarshipsResponse>> getAllStarships()
        {
            var response = await _starshipsIntagrationRefit.getAllStarships();

            if (response != null && response.IsSuccessStatusCode)
            {
                return response.Content.results;
            }

            return null;
        }

        public async Task<StarshipsResponse> getStarshipById(string id)
        {
            var responde = await _starshipsIntagrationRefit.getById(id);

            if(responde != null && responde.IsSuccessStatusCode){
                return responde.Content;
            }

            return null;
        }
    }
}