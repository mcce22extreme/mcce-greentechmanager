using Microsoft.AspNetCore.Mvc;
using GreenTechManager.WindParks.Managers;
using GreenTechManager.WindParks.Models;
using GreenTechManager.WindParks.Constants;
using Microsoft.AspNetCore.Authorization;

namespace GreenTechManager.WindParks.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]    
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(Policy = AuthConstants.RequireUserRolePolicy)]
    public class WindTurbineController : ControllerBase
    {
        private readonly IWindTurbineManager _windTurbineManager;

        public WindTurbineController(IWindTurbineManager windTurbineManager)
        {
            _windTurbineManager = windTurbineManager;
        }

        [HttpGet]
        public async Task<WindTurbineListModel[]> GetWindTurbines()
        {
            return await _windTurbineManager.GetWindTurbines();
        }

        [HttpGet("{turbineId}")]
        public async Task<WindTurbineListModel> GetWindTurbine(int turbineId)
        {
            return await _windTurbineManager.GetWindTurbine(turbineId);
        }

        [HttpPost]
        [Authorize(Policy = AuthConstants.RequireAdminRolePolicy)]
        public async Task<WindTurbineListModel> CreateWindTurbine([FromBody] WindTurbineModel model)
        {
            return await _windTurbineManager.CreateWindTurbine(model);
        }

        [HttpPut("{turbineId}")]
        [Authorize(Policy = AuthConstants.RequireAdminRolePolicy)]
        public async Task<WindTurbineListModel> UpdateWindTurbine(int turbineId, [FromBody] WindTurbineModel model)
        {
            return await _windTurbineManager.UpdateWindTurbine(turbineId, model);
        }

        [HttpDelete("{turbineId}")]
        [Authorize(Policy = AuthConstants.RequireAdminRolePolicy)]
        public async Task DeleteWindTurbine(int turbineId)
        {
            await _windTurbineManager.DeleteWindTurbine(turbineId);
        }
    }
}
