using GreenTechManager.Core.Messages;

namespace GreenTechManager.WindParks.Messages
{
    public class OperatorSavedMessage : IMessage
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Event => "OperatorSaved";
    }

    public class OperatorDeletedMessage : IMessage
    {
        public int Id { get; set; }

        public string Event => "OperatorDeleted";
    }
}
