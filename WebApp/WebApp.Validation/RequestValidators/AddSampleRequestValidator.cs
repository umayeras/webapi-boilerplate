using FluentValidation;
using WebApp.Model;

namespace WebApp.Validation.RequestValidators
{
    public class AddSampleRequestValidator : BaseValidator<AddSampleRequest>
    {
        public AddSampleRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage(GenerateMessage("Title", "not empty"));
        }
    }
}