using FluentValidation.TestHelper;
using NUnit.Framework;
using WebApp.Core.Extensions;
using WebApp.Model.Entities;
using WebApp.Validation.RequestValidators;

namespace WebApp.Tests.Validation
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class UpdateSampleRequestValidatorTests
    {
        #region members & setup

        private UpdateSampleRequestValidator validator;
        
        [SetUp]
        public void Init()
        {
            validator = new UpdateSampleRequestValidator();
        }

        #endregion

        #region invalid conditions

        [Test]
        public void Validate_ValueEmpty_HaveError()
        {
            validator.ShouldHaveValidationErrorFor(model => model.Id, 0);
            validator.ShouldHaveValidationErrorFor(model => model.Title, string.Empty);
            validator.ShouldHaveValidationErrorFor(model => model.Status, 0);
        }

        #endregion

        #region valid conditions

        [Test]
        public void Validate_ValueNotEmpty_DoNotHaveError()
        {
            validator.ShouldNotHaveValidationErrorFor(model => model.Id, 1);
            validator.ShouldNotHaveValidationErrorFor(model => model.Title, "sample-title");
            validator.ShouldNotHaveValidationErrorFor(model => model.Status, StatusType.Active.ToInt32());
        }

        #endregion
    }
}