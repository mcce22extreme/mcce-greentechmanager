using GreenTechManager.Core.Processors;
using GreenTechManager.WindParks.Managers;
using GreenTechManager.WindParks.Models;

namespace GreenTechManager.WindParks.Processors
{
    public class OperatorEventProcessor : EventProcessorBase
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public OperatorEventProcessor(string hostName, int port, IServiceScopeFactory scopeFactory)
            : base(hostName, port)
        {
            _scopeFactory = scopeFactory;

            RegisterEventHandler<OperatorSavedMessage>("OperatorSaved", OnOperatorSaved);
            RegisterEventHandler<OperatorDeletedMessage>("OperatorDeleted", OnOperatorDeleted);
        }

        private async void OnOperatorSaved(OperatorSavedMessage msg)
        {
            using var scope = _scopeFactory.CreateScope();

            var operatorManager = scope.ServiceProvider.GetRequiredService<IOperatorManager>();

            await operatorManager.CreateOrUpdateOperator(new OperatorModel
            {
                ExternalId = msg.Id,
                Name = msg.Name
            });
        }

        private async void OnOperatorDeleted(OperatorDeletedMessage msg)
        {
            using var scope = _scopeFactory.CreateScope();

            var operatorManager = scope.ServiceProvider.GetRequiredService<IOperatorManager>();

            await operatorManager.DeleteOperator(msg.Id);
        }

        private class OperatorSavedMessage
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }

        private class OperatorDeletedMessage
        {
            public int Id { get; set; }
        }
    }
}
