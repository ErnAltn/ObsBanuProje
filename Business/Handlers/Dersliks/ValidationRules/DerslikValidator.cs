
using Business.Handlers.Dersliks.Commands;
using FluentValidation;

namespace Business.Handlers.Dersliks.ValidationRules
{

    public class CreateDerslikValidator : AbstractValidator<CreateDerslikCommand>
    {
        public CreateDerslikValidator()
        {
            RuleFor(x => x.DerslikAdi).NotEmpty();
            RuleFor(x => x.Kapasite).NotEmpty();

        }
    }
    public class UpdateDerslikValidator : AbstractValidator<UpdateDerslikCommand>
    {
        public UpdateDerslikValidator()
        {
            RuleFor(x => x.DerslikAdi).NotEmpty();
            RuleFor(x => x.Kapasite).NotEmpty();

        }
    }
}