using NUnit.Framework;
using Powerplant.Core.Domain.Model.Input;
using Powerplant.Core.Domain.Validator;

namespace Powerplant.UnitTests
{
    [TestFixture]
    public class ValidationFuelsInputTests
    {
        private FuelsInput _fuelsInput;

        [SetUp]
        public void SetUp()
        {
            _fuelsInput = new FuelsInput();
        }

        [Test]
        public void Test_FuelsInput_Validation_Null()
        {
            var validator = new FuelsInputValidator();
            var validationResult = validator.Validate(_fuelsInput);

            Assert.False(validationResult.IsValid);
        }

        [Test]
        public void Test_FuelsInput_Validation_GreaterThanOrEqualToZero()
        {
            _fuelsInput.Gas = -1;
            _fuelsInput.Kerosine = -1;
            _fuelsInput.Co2 = -1;
            _fuelsInput.Wind = -1;

            var validator = new FuelsInputValidator();
            var validationResult = validator.Validate(_fuelsInput);

            Assert.False(validationResult.IsValid);
        }
    }
}
