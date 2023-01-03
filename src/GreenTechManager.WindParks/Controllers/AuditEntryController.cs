using GreenTechManager.Core.Constants;
using GreenTechManager.Core.Managers;
using GreenTechManager.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenTechManager.WindParks.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(Policy = AuthConstants.RequireAdminRolePolicy)]
    public class AuditEntryController : ControllerBase
    {
        private readonly IAuditEntryManager _auditEntryManager;

        public AuditEntryController(IAuditEntryManager auditEntryManager)
        {
            _auditEntryManager = auditEntryManager;
        }

        /// <summary>
        /// Retrieve a list of audit entries.
        /// </summary>
        /// <response code="200">Audit entries retrieved successfully.</response>
        /// <response code="401">No authentication information provided.</response>
        /// <response code="403">Not authorized to retrieve audit entries.</response>
        [HttpGet]
        public async Task<AuditEntryModel[]> GetAuditEntries()
        {
            return await _auditEntryManager.GetAuditEntries();
        }
    }
}
