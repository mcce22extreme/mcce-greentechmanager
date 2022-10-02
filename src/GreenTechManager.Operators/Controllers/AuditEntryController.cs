using GreenTechManager.Core.Constants;
using GreenTechManager.Core.Managers;
using GreenTechManager.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenTechManager.Operators.Controllers
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

        [HttpGet]
        public async Task<AuditEntryModel[]> GetAuditEntries()
        {
            return await _auditEntryManager.GetAuditEntries();
        }
    }
}
