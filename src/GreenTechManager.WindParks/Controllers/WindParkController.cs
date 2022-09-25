using GreenTechManager.WindParks.Constants;
using GreenTechManager.WindParks.Managers;
using GreenTechManager.WindParks.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenTechManager.WindParks.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(Policy = AuthConstants.RequireUserRolePolicy)]
    [Route("api/v{version:apiVersion}/[controller]")]    
    public class WindParkController : ControllerBase
    {
        private readonly IWindParkManager _windParkManager;

        public WindParkController(IWindParkManager windParkManager)
        {
            _windParkManager = windParkManager;
        }

        [HttpGet]
        public async Task<WindParkListModel[]> GetWindParks()
        {
            return await _windParkManager.GetWindParks();
        }

        [HttpGet("{windParkId}")]
        public async Task<WindParkListModel> GetWindPark(int windParkId)
        {
            return await _windParkManager.GetWindPark(windParkId);
        }

        [HttpPost]
        [Authorize(Policy = AuthConstants.RequireAdminRolePolicy)]
        public async Task<WindParkListModel> CreateWindPark([FromBody] WindParkModel model)
        {
            return await _windParkManager.CreateWindPark(model);
        }

        [HttpPut("{windParkId}")]
        [Authorize(Policy = AuthConstants.RequireAdminRolePolicy)]
        public async Task<WindParkListModel> UpdateWindPark(int windParkId, [FromBody] WindParkModel model)
        {
            return await _windParkManager.UpdateWindPark(windParkId, model);
        }

        [HttpDelete("{windParkId}")]
        [Authorize(Policy = AuthConstants.RequireAdminRolePolicy)]
        public async Task DeleteWindPark(int windParkId)
        {
            await _windParkManager.DeleteWindPark(windParkId);
        }
    }
}
