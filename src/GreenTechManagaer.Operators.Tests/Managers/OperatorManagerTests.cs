using FakeItEasy;
using GreenTechManager.Core.Enums;
using GreenTechManager.Core.Exceptions;
using GreenTechManager.Core.Messages;
using GreenTechManager.Core.Services;
using GreenTechManager.Operators.Managers;
using GreenTechManager.WindParks.Entities;
using GreenTechManager.WindParks.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace GreenTechManagaer.Operators.Tests.Managers
{
    [TestFixture]
    public class OperatorManagerTests : TestBase
    {
        private async Task<Operator> CreateOperator()
        {
            var op = new Operator
            {
                Name = Make.String(),
                Address = Make.String(),
                City = Make.String(),
                Zip = Make.Int(),
                Country = Make.String(),
                Creator = Make.String(),
                CreatedUtc = Make.DateTime(),
                Modifier = Make.String(),
                ModifiedUtc = Make.DateTime()
            };

            using var dbContext = CreateDbContext();

            await dbContext.Operators.AddAsync(op);
            await dbContext.SaveChangesAsync();

            return op;
        }

        [Test]
        public async Task GetOperators_ReturnsOperators()
        {
            var expected = await CreateOperator();

            var manager = new OperatorManager(CreateDbContext(), Mapper, A.Fake<IMessageBusService>());

            var operators = await manager.GetOperators();

            Assert.IsTrue(operators.Length == 1);

            var actual = operators.FirstOrDefault();

            Assert.IsNotNull(actual);
            Assert.That(actual.Name, Is.EqualTo(expected.Name));
            Assert.That(actual.Address, Is.EqualTo(expected.Address));
            Assert.That(actual.City, Is.EqualTo(expected.City));
            Assert.That(actual.Zip, Is.EqualTo(expected.Zip));
            Assert.That(actual.Country, Is.EqualTo(expected.Country));
        }

        [Test]
        public async Task GetOperator_ReturnsOperator()
        {
            var expected = await CreateOperator();

            var manager = new OperatorManager(CreateDbContext(), Mapper, A.Fake<IMessageBusService>());

            var actual = await manager.GetOperator(expected.Id);

            Assert.IsNotNull(actual);
            Assert.That(actual.Name, Is.EqualTo(expected.Name));
            Assert.That(actual.Address, Is.EqualTo(expected.Address));
            Assert.That(actual.City, Is.EqualTo(expected.City));
            Assert.That(actual.Zip, Is.EqualTo(expected.Zip));
            Assert.That(actual.Country, Is.EqualTo(expected.Country));
        }

        [Test]
        public async Task CreateOperator_StoresOperatorInformation()
        {
            var expected = new SaveOperatorModel
            {
                Name = Make.String(),
                Address = Make.String(),
                City = Make.String(),
                Zip = Make.Int(),
                Country = Make.String()
            };

            var manager = new OperatorManager(CreateDbContext(), Mapper, A.Fake<IMessageBusService>());

            var actual = await manager.CreateOperator(expected);

            Assert.IsNotNull(actual);
            Assert.That(actual.Name, Is.EqualTo(expected.Name));
            Assert.That(actual.Address, Is.EqualTo(expected.Address));
            Assert.That(actual.City, Is.EqualTo(expected.City));
            Assert.That(actual.Zip, Is.EqualTo(expected.Zip));
            Assert.That(actual.Country, Is.EqualTo(expected.Country));
        }

        [Test]
        public async Task CreateOperator_SetsAuditInformation()
        {
            var expected = new SaveOperatorModel
            {
                Name = Make.String(),
                Address = Make.String(),
                City = Make.String(),
                Zip = Make.Int(),
                Country = Make.String()
            };

            var manager = new OperatorManager(CreateDbContext(), Mapper, A.Fake<IMessageBusService>());

            var actual = await manager.CreateOperator(expected);

            Assert.That(actual.Creator, Is.EqualTo(UserName));
            Assert.IsNotNull(actual.CreatedUtc);
            Assert.IsNull(actual.Modifier);
            Assert.IsNull(actual.ModifiedUtc);
        }

        private bool MatchEntity(EntityMessage actual, OperatorModel expected, EventType eventType)
        {
            return actual.EventType == eventType &&
                   actual.EntityId == expected.Id &&
                   actual.EntityName == expected.Name &&
                   actual.EntityType == nameof(Operator);
        }

        [Test]
        public async Task CreateOperator_PublishesMessage()
        {
            var op = new SaveOperatorModel
            {
                Name = Make.String(),
                Address = Make.String(),
                City = Make.String(),
                Zip = Make.Int(),
                Country = Make.String()
            };

            var messageBusService = A.Fake<IMessageBusService>();

            var manager = new OperatorManager(CreateDbContext(), Mapper, messageBusService);

            var expected = await manager.CreateOperator(op);

            A.CallTo(() => messageBusService.PublishMessage(A<EntityMessage>.That.Matches(actual => MatchEntity(actual, expected, EventType.EntityCreated)))).MustHaveHappened();
        }

        public async Task CreateOperator_CreatesAuditEntry()
        {
            var op = new SaveOperatorModel
            {
                Name = Make.String(),
                Address = Make.String(),
                City = Make.String(),
                Zip = Make.Int(),
                Country = Make.String()
            };

            var manager = new OperatorManager(CreateDbContext(), Mapper, A.Fake<IMessageBusService>());

            var expected = await manager.CreateOperator(op);

            using var dbContext = CreateDbContext();

            var actual = await dbContext.AuditEntries.FirstOrDefaultAsync(x => x.EntityId == expected.Id);

            Assert.That(actual.EntityId, Is.EqualTo(expected.Id));
            Assert.That(actual.EntityName, Is.EqualTo(expected.Name));
            Assert.That(actual.EntityType, Is.EqualTo(nameof(Operator)));
            Assert.That(actual.UserName, Is.EqualTo(UserName));
            Assert.That(actual.Operation, Is.EqualTo(AuditOperation.Created));
        }

        [Test]
        public async Task UpdateOperator_UpdatesOperatorInformation()
        {
            var op = await CreateOperator();

            var manager = new OperatorManager(CreateDbContext(), Mapper, A.Fake<IMessageBusService>());

            var model = new SaveOperatorModel
            {
                Name = Make.String(),
                Address = Make.String(),
                City = Make.String(),
                Zip = Make.Int(),
                Country = Make.String()
            };

            var expected = await manager.UpdateOperator(op.Id, model);

            var actual = await manager.GetOperator(expected.Id);

            Assert.IsNotNull(actual);
            Assert.That(actual.Name, Is.EqualTo(expected.Name));
            Assert.That(actual.Address, Is.EqualTo(expected.Address));
            Assert.That(actual.City, Is.EqualTo(expected.City));
            Assert.That(actual.Zip, Is.EqualTo(expected.Zip));
            Assert.That(actual.Country, Is.EqualTo(expected.Country));
        }

        [Test]
        public async Task UpdateOperator_SetsAuditInformation()
        {
            var op = await CreateOperator();

            var manager = new OperatorManager(CreateDbContext(), Mapper, A.Fake<IMessageBusService>());

            var model = new SaveOperatorModel
            {
                Name = Make.String(),
                Address = Make.String(),
                City = Make.String(),
                Zip = Make.Int(),
                Country = Make.String()
            };

            var actual = await manager.UpdateOperator(op.Id, model);

            Assert.That(actual.Creator, Is.EqualTo(op.Creator));
            Assert.That(actual.CreatedUtc, Is.EqualTo(op.CreatedUtc));
            Assert.That(actual.Modifier, Is.EqualTo(UserName));
            Assert.That(actual.ModifiedUtc, Is.Not.EqualTo(op.ModifiedUtc));
        }

        [Test]
        public async Task UpdateOperator_PublishesMessage()
        {
            var op = await CreateOperator();

            var messageBusService = A.Fake<IMessageBusService>();

            var manager = new OperatorManager(CreateDbContext(), Mapper, messageBusService);

            var model = new SaveOperatorModel
            {
                Name = Make.String(),
                Address = Make.String(),
                City = Make.String(),
                Zip = Make.Int(),
                Country = Make.String()
            };

            var expected = await manager.UpdateOperator(op.Id, model);

            A.CallTo(() => messageBusService.PublishMessage(A<EntityMessage>.That.Matches(actual => MatchEntity(actual, expected, EventType.EntityModified)))).MustHaveHappened();
        }

        [Test]
        public async Task UpdateOperator_CreatesAuditEntry()
        {
            var op = await CreateOperator();

            var manager = new OperatorManager(CreateDbContext(), Mapper, A.Fake<IMessageBusService>());

            var model = new SaveOperatorModel
            {
                Name = Make.String(),
                Address = Make.String(),
                City = Make.String(),
                Zip = Make.Int(),
                Country = Make.String()
            };

            var expected = await manager.UpdateOperator(op.Id, model);

            using var dbContext = CreateDbContext();

            var actual = await dbContext.AuditEntries.LastOrDefaultAsync(x => x.EntityId == expected.Id);

            Assert.That(actual.EntityId, Is.EqualTo(expected.Id));
            Assert.That(actual.EntityName, Is.EqualTo(expected.Name));
            Assert.That(actual.EntityType, Is.EqualTo(nameof(Operator)));
            Assert.That(actual.UserName, Is.EqualTo(UserName));
            Assert.That(actual.Operation, Is.EqualTo(AuditOperation.Modified));
        }

        [Test]
        public async Task DeleteOperator_DeletesOperatorFromDb()
        {
            var op = await CreateOperator();

            var manager = new OperatorManager(CreateDbContext(), Mapper, A.Fake<IMessageBusService>());

            await manager.DeleteOperator(op.Id);

            Assert.ThrowsAsync<NotFoundException>(() => manager.GetOperator(op.Id));
        }

        [Test]
        public async Task Delete_CreatesAuditEntry()
        {
            var expected = await CreateOperator();
            
            var manager = new OperatorManager(CreateDbContext(), Mapper, A.Fake<IMessageBusService>());

            await manager.DeleteOperator(expected.Id);

            using var dbContext = CreateDbContext();

            var actual = await dbContext.AuditEntries.LastOrDefaultAsync(x => x.EntityId == expected.Id);

            Assert.That(actual.EntityId, Is.EqualTo(expected.Id));
            Assert.That(actual.EntityName, Is.EqualTo(expected.Name));
            Assert.That(actual.EntityType, Is.EqualTo(nameof(Operator)));
            Assert.That(actual.UserName, Is.EqualTo(UserName));
            Assert.That(actual.Operation, Is.EqualTo(AuditOperation.Deleted));
        }
    }
}
