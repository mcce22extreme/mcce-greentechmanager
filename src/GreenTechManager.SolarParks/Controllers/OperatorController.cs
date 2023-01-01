using GreenTechManager.Core.Constants;
using GreenTechManager.SolarParks.Managers;
using GreenTechManager.SolarParks.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenTechManager.SolarParks.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(Policy = AuthConstants.RequireUserRolePolicy)]
    public class OperatorController : ControllerBase
    {
        private readonly ISolarParkManager _solarParkManager;

        public OperatorController(ISolarParkManager solarParkManager)
        {
            _solarParkManager = solarParkManager;
        }

        [HttpGet]
        public async Task<OperatorModel[]> GetSolarParkOperators()
        {
            return await _solarParkManager.GetSolarParkOperators();
        }
    }
}
