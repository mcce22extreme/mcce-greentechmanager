using GreenTechManager.Core.Constants;
using GreenTechManager.WindParks.Managers;
using GreenTechManager.WindParks.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenTechManager.WindParks.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize(Policy = AuthConstants.RequireUserRolePolicy)]
    [Route("api/v{version:apiVersion}/[controller]")]    
    public class WindParkController : ControllerBase
    {
        private readonly IWindParkManager _windParkManager;

        public WindParkController(IWindParkManager windParkManager)
        {
            _windParkManager = windParkManager;
        }

        /// <summary>
        /// Retrieve a list of available wind parks.
        /// </summary>
        /// <response code="200">Wind parks retrieved successfully.</response>
        /// <response code="401">No authentication information provided.</response>
        /// <response code="403">Not authorized to retrieve wind parks.</response>
        [HttpGet]
        public async Task<WindParkListModel[]> GetWindParks()
        {
            return await _windParkManager.GetWindParks();
        }

        /// <summary>
        /// Retrieve a specific wind park
        /// </summary>
        /// <param name="windParkId" example="1">The wind park id.</param>
        /// <returns>The wind with the given id.</returns>
        /// <response code="200">Wind park retrieved successfully.</response>
        /// <response code="401">No authentication information provided.</response>
        /// <response code="403">Not authorized to retrieve wind parks.</response>
        /// <response code="404">Wind park with the given id was not found.</response>
        [HttpGet("{windParkId}")]
        public async Task<WindParkListModel> GetWindPark(int windParkId)
        {
            return await _windParkManager.GetWindPark(windParkId);
        }

        /// <summary>
        /// Create a new wind park.
        /// </summary>
        /// <param name="model">Information of the new wind park.</param>
        /// <returns>The new created wind park.</returns>
        /// <response code="200">Wind park created successfully.</response>
        /// <response code="400">Validation error occured.</response>
        /// <response code="401">No authentication information provided.</response>
        /// <response code="403">Not authorized to retrieve wind parks.</response>
        [HttpPost]
        [Authorize(Policy = AuthConstants.RequireAdminRolePolicy)]
        public async Task<WindParkListModel> CreateWindPark([FromBody] WindParkModel model)
        {
            return await _windParkManager.CreateWindPark(model);
        }

        /// <summary>
        /// Update an existing wind park.
        /// </summary>
        /// <param name="windParkId" example="1">The wind park id.</param>
        /// <param name="model">Information of the wind park.</param>
        /// <returns>The updated wind park.</returns>
        /// <response code="200">Wind park updated successfully.</response>
        /// <response code="400">Validation error occured.</response>
        /// <response code="401">No authentication information provided.</response>
        /// <response code="403">Not authorized to update wind parks.</response>
        /// <response code="404">Wind park with the given id was not found.</response>
        [HttpPut("{windParkId}")]
        [Authorize(Policy = AuthConstants.RequireAdminRolePolicy)]
        public async Task<WindParkListModel> UpdateWindPark(int windParkId, [FromBody] WindParkModel model)
        {
            return await _windParkManager.UpdateWindPark(windParkId, model);
        }

        /// <summary>
        /// Delete an existing wind park.
        /// </summary>
        /// <param name="windParkId" example="1">The wind park id.</param>
        /// <response code="200">Wind park deleted successfully.</response>        
        /// <response code="401">No authentication information provided.</response>
        /// <response code="403">Not authorized to delete wind parks.</response>
        /// <response code="404">Wind park with the given id was not found.</response>
        [HttpDelete("{windParkId}")]
        [Authorize(Policy = AuthConstants.RequireAdminRolePolicy)]
        public async Task DeleteWindPark(int windParkId)
        {
            await _windParkManager.DeleteWindPark(windParkId);
        }
    }
}
