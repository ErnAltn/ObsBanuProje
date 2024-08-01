
using Business.Handlers.Mufredats.Commands;
using FluentValidation;

namespace Business.Handlers.Mufredats.ValidationRules
{

    public class CreateMufredatValidator : AbstractValidator<CreateMufredatCommand>
    {
        public CreateMufredatValidator()
        {
            RuleFor(x => x.DersId).NotEmpty();
            RuleFor(x => x.AkedemikYilId).NotEmpty();
            RuleFor(x => x.AkedemikDonemId).NotEmpty();
            RuleFor(x => x.DersDonemi).NotEmpty();

        }
    }
    public class UpdateMufredatValidator : AbstractValidator<UpdateMufredatCommand>
    {
        public UpdateMufredatValidator()
        {
            RuleFor(x => x.DersId).NotEmpty();
            RuleFor(x => x.AkedemikYilId).NotEmpty();
            RuleFor(x => x.AkedemikDonemId).NotEmpty();
            RuleFor(x => x.DersDonemi).NotEmpty();

        }
    }
}