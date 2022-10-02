using System.ComponentModel.DataAnnotations;
using GreenTechManager.Core.Enums;

namespace GreenTechManager.Core.Entities
{
    public class AuditEntry : EntityBase
    {
        [Required]
        public int EntityId { get; set; }

        [Required]
        public string EntityType { get; set; }

        [Required]
        public string EntityName { get; set; }

        [Required]
        public DateTime DateUtc { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public AuditOperation Operation { get; set; }
    }
}
