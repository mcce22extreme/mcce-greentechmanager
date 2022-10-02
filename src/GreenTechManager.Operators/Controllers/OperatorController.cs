using GreenTechManager.Core.Constants;
using GreenTechManager.Core.Services;
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
        private static readonly HttpClient HttpClient = new HttpClient();
        private readonly IOperatorManager _operatorManager;
        private readonly IMessageBusService _messageBusService;

        public OperatorController(IOperatorManager operatorManager, IMessageBusService messageBusService)
        {
            _operatorManager = operatorManager;
            _messageBusService = messageBusService;
        }

        [HttpGet]
        public async Task<OperatorListModel[]> GetOperators()
        {
            return await _operatorManager.GetOperators();
        }

        [HttpGet("{operatorId}")]
        public async Task<OperatorListModel> GetOperator(int operatorId)
        {
            return await _operatorManager.GetOperator(operatorId);
        }

        [HttpPost]
        [Authorize(Policy = AuthConstants.RequireAdminRolePolicy)]
        public async Task<OperatorListModel> CreateOperator([FromBody] OperatorModel model)
        {
            return await _operatorManager.CreateOperator(model);
        }

        [HttpPut("{operatorId}")]
        [Authorize(Policy = AuthConstants.RequireAdminRolePolicy)]
        public async Task<OperatorListModel> UpdateOperator(int operatorId, [FromBody] OperatorModel model)
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
