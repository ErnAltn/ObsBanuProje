
using Business.Handlers.DersProgramis.Commands;
using FluentValidation;

namespace Business.Handlers.DersProgramis.ValidationRules
{

    public class CreateDersProgramiValidator : AbstractValidator<CreateDersProgramiCommand>
    {
        public CreateDersProgramiValidator()
        {
            RuleFor(x => x.DerslikId).NotEmpty();
            RuleFor(x => x.DersGunuId).NotEmpty();
            RuleFor(x => x.DersSaati).NotEmpty();

        }
    }
    public class UpdateDersProgramiValidator : AbstractValidator<UpdateDersProgramiCommand>
    {
        public UpdateDersProgramiValidator()
        {
            RuleFor(x => x.DerslikId).NotEmpty();
            RuleFor(x => x.DersGunuId).NotEmpty();
            RuleFor(x => x.DersSaati).NotEmpty();

        }
    }
}