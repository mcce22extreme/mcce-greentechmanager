using GreenTechManager.Core.Constants;
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

        [HttpGet("{solarParkId}")]
        public async Task<SolarParkListModel> GetSolarPark(int solarParkId)
        {
            return await _solarParkManager.GetSolarPark(solarParkId);
        }

        [HttpPost]
        [Authorize(Policy = AuthConstants.RequireAdminRolePolicy)]
        public async Task<SolarParkListModel> CreateSolarPark([FromBody] SolarParkModel model)
        {
            return await _solarParkManager.CreateSolarPark(model);
        }

        [HttpPut("{solarParkId}")]
        [Authorize(Policy = AuthConstants.RequireAdminRolePolicy)]
        public async Task<SolarParkListModel> UpdateSolarPark(int solarParkId, [FromBody] SolarParkModel model)
        {
            return await _solarParkManager.UpdateSolarPark(solarParkId, model);
        }

        [HttpDelete("{solarParkId}")]
        [Authorize(Policy = AuthConstants.RequireAdminRolePolicy)]
        public async Task DeleteSolarPark(int solarParkId)
        {
            await _solarParkManager.DeleteSolarPark(solarParkId);
        }
    }
}
