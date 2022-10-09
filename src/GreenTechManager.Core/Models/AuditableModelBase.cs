namespace GreenTechManager.Core.Models
{
    public class AuditableModelBase
    {
        public DateTime? CreatedUtc { get; set; }

        public string Creator { get; set; }

        public DateTime? ModifiedUtc { get; set; }

        public string Modifier { get; set; }
    }
}
