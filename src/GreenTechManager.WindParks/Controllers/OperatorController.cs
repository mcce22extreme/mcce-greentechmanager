using GreenTechManager.Core.Constants;
using GreenTechManager.WindParks.Managers;
using GreenTechManager.WindParks.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenTechManager.WindParks.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(Policy = AuthConstants.RequireUserRolePolicy)]
    public class OperatorController : ControllerBase
    {
        private readonly IWindParkManager _windParkManager;

        public OperatorController(IWindParkManager windParkManager)
        {
            _windParkManager = windParkManager;
        }

        /// <summary>
        /// Retrieve a list of available wind park operators.
        /// </summary>
        /// <response code="200">Wind park operators retrieved successfully.</response>
        /// <response code="401">No authentication information provided.</response>
        /// <response code="403">Not authorized to retrieve wind park operators.</response>
        [HttpGet]
        public async Task<OperatorModel[]> GetWindParkOperators()
        {
            return await _windParkManager.GetWindParkOperators();
        }
    }
}
