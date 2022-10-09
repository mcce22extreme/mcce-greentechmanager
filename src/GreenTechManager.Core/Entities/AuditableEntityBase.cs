namespace GreenTechManager.Core.Entities
{
    public interface IAuditableEntity : IEntity
    {
        string EntityName { get; }

        DateTime? CreatedUtc { get; }

        string Creator { get; }

        DateTime? ModifiedUtc { get; }

        string Modifier { get; }
    }

    public abstract class AuditableEntityBase : EntityBase, IAuditableEntity
    {
        public abstract string EntityName { get; }

        public DateTime? CreatedUtc { get; set; }

        public string Creator { get; set; }

        public DateTime? ModifiedUtc { get; set; }

        public string Modifier { get; set; }
    }
}
