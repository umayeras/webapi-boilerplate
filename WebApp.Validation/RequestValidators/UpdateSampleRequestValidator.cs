using FluentValidation;
using WebApp.Model.Requests;

namespace WebApp.Validation.RequestValidators
{
    public class UpdateSampleRequestValidator : BaseValidator<UpdateSampleRequest>
    {
        public UpdateSampleRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(GenerateMessage("Id", "not empty"));

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage(GenerateMessage("Title", "not empty"));

            RuleFor(x => x.Status)
                .NotEmpty()
                .WithMessage(GenerateMessage("Status", "not empty"));
        }
    }
}