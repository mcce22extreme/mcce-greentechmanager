using GreenTechManager.SolarParks.Constants;
using GreenTechManager.SolarParks.Models;
using GreenTechManager.WindParks.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenTechManager.WindParks.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(Policy = AuthConstants.RequireUserRolePolicy)]
    public class SolarArrayController : ControllerBase
    {
        private readonly ISolarArrayManager _solarArrayManager;

        public SolarArrayController(ISolarArrayManager solarArrayManager)
        {
            _solarArrayManager = solarArrayManager;
        }

        [HttpGet]
        public async Task<SolarArrayListModel[]> GetSolarArrays()
        {
            return await _solarArrayManager.GetSolarArrays();
        }

        [HttpGet("{arrayId}")]
        public async Task<SolarArrayListModel> GetSolarArray(int arrayId)
        {
            return await _solarArrayManager.GetSolarArray(arrayId);
        }

        [HttpPost]
        [Authorize(Policy = AuthConstants.RequireAdminRolePolicy)]
        public async Task<SolarArrayListModel> CreateSolarArray([FromBody] SolarParkModel model)
        {
            return await _solarArrayManager.CreateSolarArray(model);
        }

        [HttpPut("{arrayId}")]
        [Authorize(Policy = AuthConstants.RequireAdminRolePolicy)]
        public async Task<SolarArrayListModel> UpdateSolarArray(int arrayId, [FromBody] SolarParkModel model)
        {
            return await _solarArrayManager.UpdateSolarArray(arrayId, model);
        }

        [HttpDelete("{arrayId}")]
        [Authorize(Policy = AuthConstants.RequireAdminRolePolicy)]
        public async Task DeleteSolarPark(int arrayId)
        {
            await _solarArrayManager.DeleteSolarArray(arrayId);
        }
    }
}
