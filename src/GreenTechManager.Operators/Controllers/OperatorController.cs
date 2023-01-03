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

        /// <summary>
        /// Retrieve a list of available operators.
        /// </summary>
        /// <response code="200">Operators retrieved successfully.</response>
        /// <response code="401">No authentication information provided.</response>
        /// <response code="403">Not authorized to retrieve operators.</response>
        [HttpGet]
        public async Task<OperatorModel[]> GetOperators()
        {
            return await _operatorManager.GetOperators();
        }

        /// <summary>
        /// Retrieve a specific operator.
        /// </summary>
        /// <param name="operatorId" example="1">The operator id.</param>
        /// <returns>The operator with the given id.</returns>
        /// <response code="200">Operator retrieved successfully.</response>
        /// <response code="401">No authentication information provided.</response>
        /// <response code="403">Not authorized to retrieve operators.</response>
        /// <response code="404">Operator with the given id was not found.</response>
        [HttpGet("{operatorId}")]
        public async Task<OperatorModel> GetOperator(int operatorId)
        {
            return await _operatorManager.GetOperator(operatorId);
        }

        /// <summary>
        /// Create a new operator.
        /// </summary>
        /// <param name="model">Information of the new operator.</param>
        /// <returns>The new created operator.</returns>
        /// <response code="200">Operator created successfully.</response>
        /// <response code="400">Validation error occured.</response>
        /// <response code="401">No authentication information provided.</response>
        /// <response code="403">Not authorized to create operators.</response>
        [HttpPost]
        [Authorize(Policy = AuthConstants.RequireAdminRolePolicy)]
        public async Task<OperatorModel> CreateOperator([FromBody] SaveOperatorModel model)
        {
            return await _operatorManager.CreateOperator(model);
        }

        /// <summary>
        /// Update an existing operator.
        /// </summary>
        /// <param name="operatorId" example="1">The operator id.</param>
        /// <param name="model">Information of the operator.</param>
        /// <returns>The updated operator.</returns>
        /// <response code="200">Operator updated successfully.</response>
        /// <response code="400">Validation error occured.</response>
        /// <response code="401">No authentication information provided.</response>
        /// <response code="403">Not authorized to update operators.</response>
        /// <response code="404">Operator with the given id was not found.</response>
        [HttpPut("{operatorId}")]
        [Authorize(Policy = AuthConstants.RequireAdminRolePolicy)]
        public async Task<OperatorModel> UpdateOperator(int operatorId, [FromBody] SaveOperatorModel model)
        {
            return await _operatorManager.UpdateOperator(operatorId, model);
        }

        /// <summary>
        /// Delete an existing operator.
        /// </summary>
        /// <param name="operatorId" example="1">The operator id.</param>
        /// <response code="200">Operator deleted successfully.</response>        
        /// <response code="401">No authentication information provided.</response>
        /// <response code="403">Not authorized to delete operators.</response>
        /// <response code="404">Operator with the given id was not found.</response>
        [HttpDelete("{operatorId}")]
        [Authorize(Policy = AuthConstants.RequireAdminRolePolicy)]
        public async Task DeleteOperator(int operatorId)
        {
            await _operatorManager.DeleteOperator(operatorId);
        }
    }
}
