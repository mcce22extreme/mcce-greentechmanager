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

        /// <summary>
        /// Retrieve a list of available solar parks.
        /// </summary>
        /// <response code="200">Solar parks retrieved successfully.</response>
        /// <response code="401">No authentication information provided.</response>
        /// <response code="403">Not authorized to retrieve solar parks.</response>
        [HttpGet]
        public async Task<SolarParkListModel[]> GetSolarParks()
        {
            return await _solarParkManager.GetSolarParks();
        }

        /// <summary>
        /// Retrieve a specific solar park
        /// </summary>
        /// <param name="solarParkId" example="1">The solar park id.</param>
        /// <returns>The operator with the given id.</returns>
        /// <response code="200">Solar park retrieved successfully.</response>
        /// <response code="401">No authentication information provided.</response>
        /// <response code="403">Not authorized to retrieve solar parks.</response>
        /// <response code="404">Solar park with the given id was not found.</response>
        [HttpGet("{solarParkId}")]
        public async Task<SolarParkListModel> GetSolarPark(int solarParkId)
        {
            return await _solarParkManager.GetSolarPark(solarParkId);
        }

        /// <summary>
        /// Create a new solar park.
        /// </summary>
        /// <param name="model">Information of the new solar park.</param>
        /// <returns>The new created solar park.</returns>
        /// <response code="200">Solar park created successfully.</response>
        /// <response code="400">Validation error occured.</response>
        /// <response code="401">No authentication information provided.</response>
        /// <response code="403">Not authorized to retrieve solar parks.</response>
        [HttpPost]
        [Authorize(Policy = AuthConstants.RequireAdminRolePolicy)]
        public async Task<SolarParkListModel> CreateSolarPark([FromBody] SolarParkModel model)
        {
            return await _solarParkManager.CreateSolarPark(model);
        }

        /// <summary>
        /// Update an existing solar park.
        /// </summary>
        /// <param name="solarParkId" example="1">The solar park id.</param>
        /// <param name="model">Information of the solar park.</param>
        /// <returns>The updated solar park.</returns>
        /// <response code="200">Solar park updated successfully.</response>
        /// <response code="400">Validation error occured.</response>
        /// <response code="401">No authentication information provided.</response>
        /// <response code="403">Not authorized to update solar parks.</response>
        /// <response code="404">Solar park with the given id was not found.</response>
        [HttpPut("{solarParkId}")]
        [Authorize(Policy = AuthConstants.RequireAdminRolePolicy)]
        public async Task<SolarParkListModel> UpdateSolarPark(int solarParkId, [FromBody] SolarParkModel model)
        {
            return await _solarParkManager.UpdateSolarPark(solarParkId, model);
        }

        /// <summary>
        /// Delete an existing solar park.
        /// </summary>
        /// <param name="solarParkId" example="1">The solar park id.</param>
        /// <response code="200">Solar park deleted successfully.</response>        
        /// <response code="401">No authentication information provided.</response>
        /// <response code="403">Not authorized to delete solar parks.</response>
        /// <response code="404">Solar park with the given id was not found.</response>
        [HttpDelete("{solarParkId}")]
        [Authorize(Policy = AuthConstants.RequireAdminRolePolicy)]
        public async Task DeleteSolarPark(int solarParkId)
        {
            await _solarParkManager.DeleteSolarPark(solarParkId);
        }
    }
}
