using GreenTechManager.Core.Enums;
using GreenTechManager.Core.Messages;
using GreenTechManager.Core.Processors;
using GreenTechManager.SolarParks.Entities;

namespace GreenTechManager.SolarParks.Processors
{
    public class EntityEventProcessor : EventProcessorBase
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public EntityEventProcessor(string hostName, int port, IServiceScopeFactory scopeFactory)
            : base(hostName, port)
        {
            _scopeFactory = scopeFactory;

            RegisterEventHandler<EntityMessage>(EventType.EntityCreated, OnEntityCreated);
            RegisterEventHandler<EntityMessage>(EventType.EntityModified, OnEntityModified);
            RegisterEventHandler<EntityMessage>(EventType.EntityDeleted, OnEntityDeleted);
        }

        private void OnEntityCreated(EntityMessage msg)
        {
            if (msg.EntityType == nameof(Operator))
            {

            }
        }

        private void OnEntityModified(EntityMessage msg)
        {
            if (msg.EntityType == nameof(Operator))
            {

            }
        }

        private void OnEntityDeleted(EntityMessage msg)
        {
            if (msg.EntityType == nameof(Operator))
            {

            }
        }

        //private async void OnMessageReceived(EntityMessage msg)
        //{
        //    if (msg.EntityType == nameof(Operator))
        //    {
        //        using var scope = _scopeFactory.CreateScope();

        //        var operatorManager = scope.ServiceProvider.GetRequiredService<IOperatorManager>();

        //        await operatorManager.CreateOrUpdateOperator(new OperatorModel
        //        {
        //            ExternalId = msg.EntityId,
        //            Name = msg.EntityName
        //        });
        //    }
        //}
    }
}
