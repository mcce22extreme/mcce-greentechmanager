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

        [HttpGet]
        public async Task<OperatorModel[]> GetWindParkOperators()
        {
            return await _windParkManager.GetWindParkOperators();
        }
    }
}
