using FakeItEasy;
using GreenTechManager.Operators.Controllers;
using GreenTechManager.Operators.Managers;
using GreenTechManager.WindParks.Models;
using NUnit.Framework;

namespace GreenTechManagaer.Operators.Tests.Controllers
{
    [TestFixture]
    public class OperatorControllerTests : TestBase
    {
        [Test]
        public async Task GetOperators_CallsManagerGetOperators()
        {
            var manager = A.Fake<IOperatorManager>();

            var controller = new OperatorController(manager);

            await controller.GetOperators();

            A.CallTo(() => manager.GetOperators()).MustHaveHappened();
        }

        [Test]
        public async Task GetOperator_CallsManagerGetOperator()
        {
            var operatorId = Make.Int();

            var manager = A.Fake<IOperatorManager>();

            var controller = new OperatorController(manager);

            await controller.GetOperator(operatorId);

            A.CallTo(() => manager.GetOperator(operatorId)).MustHaveHappened();
        }

        [Test]
        public async Task CreateOperator_CallsManagerCreateOperator()
        {
            var model = new SaveOperatorModel();

            var manager = A.Fake<IOperatorManager>();

            var controller = new OperatorController(manager);

            await controller.CreateOperator(model);

            A.CallTo(() => manager.CreateOperator(model)).MustHaveHappened();
        }

        [Test]
        public async Task UpdateOperator_CallsManagerUpdateOperator()
        {
            var operatorId = Make.Int();
            var model = new SaveOperatorModel();

            var manager = A.Fake<IOperatorManager>();

            var controller = new OperatorController(manager);

            await controller.UpdateOperator(operatorId, model);

            A.CallTo(() => manager.UpdateOperator(operatorId, model)).MustHaveHappened();
        }

        [Test]
        public async Task DeleteOperator_CallsManagerDeleteOperator()
        {
            var operatorId = Make.Int();

            var manager = A.Fake<IOperatorManager>();

            var controller = new OperatorController(manager);

            await controller.DeleteOperator(operatorId);

            A.CallTo(() => manager.DeleteOperator(operatorId)).MustHaveHappened();
        }
    }
}
