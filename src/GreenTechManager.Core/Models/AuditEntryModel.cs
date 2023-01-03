using GreenTechManager.Core.Enums;

namespace GreenTechManager.Core.Models
{
    public class AuditEntryModel
    {
        /// <summary>
        /// The identifier of the entity.
        /// </summary>
        public int EntityId { get; set; }

        /// <summary>
        /// The type of the entity.
        /// </summary>
        public string EntityType { get; set; }

        /// <summary>
        /// The name of the entity.
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// The create date of the audit entry.
        /// </summary>
        public DateTime DateUtc { get; set; }

        /// <summary>
        /// The username of the user that triggered the operation.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The type of operation.
        /// </summary>
        public AuditOperation Operation { get; set; }
    }
}
