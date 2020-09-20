using FluentValidation.TestHelper;
using NUnit.Framework;
using WebApp.Validation.RequestValidators;

namespace WebApp.Tests.Validation
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class AddSampleRequestValidatorTests
    {
        #region members & setup

        private AddSampleRequestValidator validator;
        
        [SetUp]
        public void Init()
        {
            validator = new AddSampleRequestValidator();
        }

        #endregion

        #region invalid conditions

        [Test]
        public void Validate_ValueEmpty_HaveError()
        {
            validator.ShouldHaveValidationErrorFor(model => model.Title, string.Empty);
        }

        #endregion

        #region valid conditions

        [Test]
        public void Validate_ValueNotEmpty_DoNotHaveError()
        {
            validator.ShouldNotHaveValidationErrorFor(model => model.Title, "sample-title");
        }

        #endregion
    }
}