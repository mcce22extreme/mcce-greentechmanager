using GreenTechManager.Core.Enums;
using GreenTechManager.Core.Messages;
using GreenTechManager.Core.Processors;
using GreenTechManager.WindParks.Entities;
using GreenTechManager.WindParks.Managers;
using GreenTechManager.WindParks.Models;

namespace GreenTechManager.WindParks.Processors
{
    public class EntityEventProcessor : EventProcessorBase
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public EntityEventProcessor(string hostName, int port, IServiceScopeFactory scopeFactory)
            : base(hostName, port)
        {
            _scopeFactory = scopeFactory;

            RegisterEventHandler<EntityMessage>(EventType.EntityCreated, CreateOrUpdateOperator);
            RegisterEventHandler<EntityMessage>(EventType.EntityModified, CreateOrUpdateOperator);
            RegisterEventHandler<EntityMessage>(EventType.EntityDeleted, DeleteOperator);
        }

        private async void CreateOrUpdateOperator(EntityMessage msg)
        {
            if (msg.EntityType == nameof(Operator))
            {
                using var scope = _scopeFactory.CreateScope();

                var operatorManager = scope.ServiceProvider.GetRequiredService<IOperatorManager>();

                await operatorManager.CreateOrUpdateOperator(new OperatorModel
                {
                    ExternalId = msg.EntityId,
                    Name = msg.EntityName
                });
            }
        }

        private async void DeleteOperator(EntityMessage msg)
        {
            if (msg.EntityType == nameof(Operator))
            {
                using var scope = _scopeFactory.CreateScope();

                var operatorManager = scope.ServiceProvider.GetRequiredService<IOperatorManager>();

                await operatorManager.DeleteOperator(msg.EntityId);
            }
        }
    }
}
