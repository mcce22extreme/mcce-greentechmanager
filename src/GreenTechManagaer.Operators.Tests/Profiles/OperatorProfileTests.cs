using GreenTechManager.WindParks.Entities;
using GreenTechManager.WindParks.Models;
using NUnit.Framework;

namespace GreenTechManagaer.Operators.Tests.Profiles
{
    [TestFixture]
    public class OperatorProfileTests : TestBase
    {
        [Test]
        public void Map_Operator_To_OperatorModel_MapsAllInformation()
        {
            var expected = new Operator
            {
                Id = Make.Int(),
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

            var actual = Mapper.Map<OperatorModel>(expected);

            Assert.That(actual.Id, Is.EqualTo(expected.Id));
            Assert.That(actual.Name, Is.EqualTo(expected.Name));
            Assert.That(actual.Address, Is.EqualTo(expected.Address));
            Assert.That(actual.City, Is.EqualTo(expected.City));
            Assert.That(actual.Zip, Is.EqualTo(expected.Zip));
            Assert.That(actual.Country, Is.EqualTo(expected.Country));
            Assert.That(actual.Creator, Is.EqualTo(expected.Creator));
            Assert.That(actual.CreatedUtc, Is.EqualTo(expected.CreatedUtc));
            Assert.That(actual.Modifier, Is.EqualTo(expected.Modifier));
            Assert.That(actual.ModifiedUtc, Is.EqualTo(expected.ModifiedUtc));
        }

        [Test]
        public void Map_SaveOperatorModel_To_Operator_MapsAllInformation()
        {
            var expected = new SaveOperatorModel
            {
                Name = Make.String(),
                Address = Make.String(),
                City = Make.String(),
                Zip = Make.Int(),
                Country = Make.String(),
            };

            var actual = Mapper.Map<Operator>(expected);

            Assert.That(actual.Name, Is.EqualTo(expected.Name));
            Assert.That(actual.Address, Is.EqualTo(expected.Address));
            Assert.That(actual.City, Is.EqualTo(expected.City));
            Assert.That(actual.Zip, Is.EqualTo(expected.Zip));
            Assert.That(actual.Country, Is.EqualTo(expected.Country));
        }
    }
}
