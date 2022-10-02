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

        public DateTime? CreatedUtc { get; internal set; }

        public string Creator { get; internal set; }

        public DateTime? ModifiedUtc { get; internal set; }

        public string Modifier { get; internal set; }
    }
}
