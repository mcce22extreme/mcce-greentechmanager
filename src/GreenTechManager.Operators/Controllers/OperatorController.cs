using GreenTechManager.Core.Constants;
using GreenTechManager.Operators.Managers;
using GreenTechManager.WindParks.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenTechManager.Operators.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(Policy = AuthConstants.RequireUserRolePolicy)]
    public class OperatorController : ControllerBase
    {
        private readonly IOperatorManager _operatorManager;

        public OperatorController(IOperatorManager operatorManager)
        {
            _operatorManager = operatorManager;
        }

        [HttpGet]
        public async Task<OperatorModel[]> GetOperators()
        {
            return await _operatorManager.GetOperators();
        }

        [HttpGet("{operatorId}")]
        public async Task<OperatorModel> GetOperator(int operatorId)
        {
            return await _operatorManager.GetOperator(operatorId);
        }

        [HttpPost]
        [Authorize(Policy = AuthConstants.RequireAdminRolePolicy)]
        public async Task<OperatorModel> CreateOperator([FromBody] SaveOperatorModel model)
        {
            return await _operatorManager.CreateOperator(model);
        }

        [HttpPut("{operatorId}")]
        [Authorize(Policy = AuthConstants.RequireAdminRolePolicy)]
        public async Task<OperatorModel> UpdateOperator(int operatorId, [FromBody] SaveOperatorModel model)
        {
            return await _operatorManager.UpdateOperator(operatorId, model);
        }

        [HttpDelete("{operatorId}")]
        [Authorize(Policy = AuthConstants.RequireAdminRolePolicy)]
        public async Task DeleteOperator(int operatorId)
        {
            await _operatorManager.DeleteOperator(operatorId);
        }
    }
}
