using GreenTechManager.Core.Enums;

namespace GreenTechManager.Core.Messages
{
    public interface IEventMessage
    {
        EventType EventType { get; }

        DateTime EventDateUtc { get; }
    }

    public class EventMessage : IEventMessage
    {
        public EventType EventType { get; set; }

        public DateTime EventDateUtc { get; set; }
    }
}
