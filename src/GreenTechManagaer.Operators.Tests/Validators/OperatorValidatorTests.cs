using System.Collections;
using GreenTechManager.Operators.Validators;
using GreenTechManager.WindParks.Models;
using NUnit.Framework;

namespace GreenTechManagaer.Operators.Tests.Validators
{
    [TestFixture]
    public class OperatorValidatorTests
    {
        private const string VALIDATOR_NOTEMPTY = "NotEmptyValidator";
        private const string VALIDATOR_MAXLENGTH = "MaximumLengthValidator";
        private const string VALIDATOR_GREATERTHEN = "GreaterThanValidator";

        [Test]
        public async Task Validate_ForValidModel_ReturnsNoBrokenRules()
        {
            var model = new SaveOperatorModel
            {
                Name = Make.String(),
                Address = Make.String(),
                City = Make.String(),
                Zip = Make.Int(),
                Country = Make.String()
            };

            var validator = new SaveOperatorValidator();

            var result = await validator.ValidateAsync(model);

            Assert.IsTrue(result.IsValid);
        }

        public static IEnumerable InvalidOperatorsTestCases
        {
            get
            {
                #region Name
                yield return new TestCaseData(new SaveOperatorModel
                {
                    Address = Make.String(),
                    City = Make.String(),
                    Zip = Make.Int(),
                    Country = Make.String()
                }, new BrokenRule(nameof(SaveOperatorModel.Name), VALIDATOR_NOTEMPTY));

                yield return new TestCaseData(new SaveOperatorModel
                {
                    Name = string.Empty,
                    Address = Make.String(),
                    City = Make.String(),
                    Zip = Make.Int(),
                    Country = Make.String()
                }, new BrokenRule(nameof(SaveOperatorModel.Name), VALIDATOR_NOTEMPTY));

                yield return new TestCaseData(new SaveOperatorModel
                {
                    Name = Make.String(600),
                    Address = Make.String(),
                    City = Make.String(),
                    Zip = Make.Int(),
                    Country = Make.String()
                }, new BrokenRule(nameof(SaveOperatorModel.Name), VALIDATOR_MAXLENGTH));
                #endregion

                #region Address
                yield return new TestCaseData(new SaveOperatorModel
                {
                    Name = Make.String(),
                    City = Make.String(),
                    Zip = Make.Int(),
                    Country = Make.String()
                }, new BrokenRule(nameof(SaveOperatorModel.Address), VALIDATOR_NOTEMPTY));

                yield return new TestCaseData(new SaveOperatorModel
                {
                    Name = Make.String(),
                    Address = string.Empty,
                    City = Make.String(),
                    Zip = Make.Int(),
                    Country = Make.String()
                }, new BrokenRule(nameof(SaveOperatorModel.Address), VALIDATOR_NOTEMPTY));

                yield return new TestCaseData(new SaveOperatorModel
                {
                    Name = Make.String(),
                    Address = Make.String(600),
                    City = Make.String(),
                    Zip = Make.Int(),
                    Country = Make.String()
                }, new BrokenRule(nameof(SaveOperatorModel.Address), VALIDATOR_MAXLENGTH));
                #endregion

                #region City
                yield return new TestCaseData(new SaveOperatorModel
                {
                    Name = Make.String(),
                    Address = Make.String(),
                    Zip = Make.Int(),
                    Country = Make.String()
                }, new BrokenRule(nameof(SaveOperatorModel.City), VALIDATOR_NOTEMPTY));

                yield return new TestCaseData(new SaveOperatorModel
                {
                    Name = Make.String(),
                    Address = Make.String(),
                    City = string.Empty,
                    Zip = Make.Int(),
                    Country = Make.String()
                }, new BrokenRule(nameof(SaveOperatorModel.City), VALIDATOR_NOTEMPTY));

                yield return new TestCaseData(new SaveOperatorModel
                {
                    Name = Make.String(),
                    Address = Make.String(),
                    City = Make.String(600),
                    Zip = Make.Int(),
                    Country = Make.String()
                }, new BrokenRule(nameof(SaveOperatorModel.City), VALIDATOR_MAXLENGTH));
                #endregion

                #region Zip
                yield return new TestCaseData(new SaveOperatorModel
                {
                    Name = Make.String(),
                    Address = Make.String(),
                    City = Make.String(),
                    Zip = 0,
                    Country = Make.String()
                }, new BrokenRule(nameof(SaveOperatorModel.Zip), VALIDATOR_GREATERTHEN));
                #endregion

                #region Country
                yield return new TestCaseData(new SaveOperatorModel
                {
                    Name = Make.String(),
                    Address = Make.String(),
                    City = Make.String(),
                    Zip = Make.Int()
                }, new BrokenRule(nameof(SaveOperatorModel.Country), VALIDATOR_NOTEMPTY));

                yield return new TestCaseData(new SaveOperatorModel
                {
                    Name = Make.String(),
                    Address = Make.String(),
                    City = Make.String(),
                    Zip = Make.Int(),
                    Country = string.Empty
                }, new BrokenRule(nameof(SaveOperatorModel.Country), VALIDATOR_NOTEMPTY));

                yield return new TestCaseData(new SaveOperatorModel
                {
                    Name = Make.String(),
                    Address = Make.String(),
                    City = Make.String(),
                    Zip = Make.Int(),
                    Country = Make.String(600)
                }, new BrokenRule(nameof(SaveOperatorModel.Country), VALIDATOR_MAXLENGTH));
                #endregion
            }
        }

        [Test]
        [TestCaseSource(nameof(InvalidOperatorsTestCases))]
        public async Task Validate_ForInvalidModel_ReturnsBrokenRules(SaveOperatorModel model, BrokenRule brokenRule)
        {
            var validator = new SaveOperatorValidator();

            var result = await validator.ValidateAsync(model);

            Assert.IsFalse(result.IsValid);

            var error = result.Errors.FirstOrDefault(x => x.PropertyName == brokenRule.PropertyName && x.ErrorCode == brokenRule.ErrorCode);

            Assert.IsNotNull(error);
        }
    }

    public class BrokenRule
    {
        public string PropertyName { get; set; }

        public string ErrorCode { get; set; }

        public BrokenRule(string propertyName, string validator)
        {
            PropertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName));
            ErrorCode = validator ?? throw new ArgumentNullException(nameof(validator));
        }
    }
}
