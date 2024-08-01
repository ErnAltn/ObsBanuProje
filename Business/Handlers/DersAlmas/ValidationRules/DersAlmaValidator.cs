
using Business.Handlers.DersAlmas.Commands;
using FluentValidation;

namespace Business.Handlers.DersAlmas.ValidationRules
{

    public class CreateDersAlmaValidator : AbstractValidator<CreateDersAlmaCommand>
    {
        public CreateDersAlmaValidator()
        {
            RuleFor(x => x.OgrenciId).NotEmpty();
            RuleFor(x => x.DersDurumId).NotEmpty();

        }
    }
    public class UpdateDersAlmaValidator : AbstractValidator<UpdateDersAlmaCommand>
    {
        public UpdateDersAlmaValidator()
        {
            RuleFor(x => x.OgrenciId).NotEmpty();
            RuleFor(x => x.DersDurumId).NotEmpty();

        }
    }
}