using GreenTechManager.Core.Enums;

namespace GreenTechManager.Core.Models
{
    public class AuditEntryModel
    {
        public int EntityId { get; set; }

        public string EntityType { get; set; }

        public string EntityName { get; set; }

        public DateTime DateUtc { get; set; }

        public string UserName { get; set; }

        public AuditOperation Operation { get; set; }
    }
}
