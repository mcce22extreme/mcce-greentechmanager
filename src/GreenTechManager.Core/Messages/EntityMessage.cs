using GreenTechManager.Core.Enums;

namespace GreenTechManager.Core.Messages
{
    public interface IEntityMessage : IEventMessage
    {
        int EntityId { get; }

        string EntityType { get; }

        string EntityName { get; }
    }

    public class EntityMessage : IEntityMessage
    {
        public int EntityId { get; set; }

        public string EntityType { get; set; }

        public string EntityName { get; set; }

        public EventType EventType { get; }

        public DateTime EventDateUtc => DateTime.UtcNow;

        public EntityMessage(EventType eventType)
        {
            EventType = eventType;
        }
    }
}
