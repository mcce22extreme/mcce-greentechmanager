using GreenTechManager.SolarParks.Constants;
using GreenTechManager.SolarParks.Managers;
using GreenTechManager.SolarParks.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenTechManager.WindParks.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(Policy = AuthConstants.RequireUserRolePolicy)]
    public class SolarParkController : ControllerBase
    {
        private readonly ISolarParkManager _solarParkManager;

        public SolarParkController(ISolarParkManager solarParkManager)
        {
            _solarParkManager = solarParkManager;
        }

        [HttpGet]
        public async Task<SolarParkListModel[]> GetSolarParks()
        {
            return await _solarParkManager.GetSolarParks();
        }

        [HttpGet("{parkId}")]
        public async Task<SolarParkListModel> GetSolarPark(int parkId)
        {
            return await _solarParkManager.GetSolarPark(parkId);
        }

        [HttpPost]
        [Authorize(Policy = AuthConstants.RequireAdminRolePolicy)]
        public async Task<SolarParkListModel> CreateSolarPark([FromBody] SolarParkModel model)
        {
            return await _solarParkManager.CreateSolarPark(model);
        }

        [HttpPut("{parkId}")]
        [Authorize(Policy = AuthConstants.RequireAdminRolePolicy)]
        public async Task<SolarParkListModel> UpdateSolarPark(int parkId, [FromBody] SolarParkModel model)
        {
            return await _solarParkManager.UpdateSolarPark(parkId, model);
        }

        [HttpDelete("{parkId}")]
        [Authorize(Policy = AuthConstants.RequireAdminRolePolicy)]
        public async Task DeleteSolarPark(int parkId)
        {
            await _solarParkManager.DeleteSolarPark(parkId);
        }
    }
}
