
using Business.Handlers.DersAcmas.Commands;
using FluentValidation;

namespace Business.Handlers.DersAcmas.ValidationRules
{

    public class CreateDersAcmaValidator : AbstractValidator<CreateDersAcmaCommand>
    {
        public CreateDersAcmaValidator()
        {
            RuleFor(x => x.AkademikDonemId).NotEmpty();
            RuleFor(x => x.MufredatId).NotEmpty();
            RuleFor(x => x.OgrElmId).NotEmpty();
            RuleFor(x => x.Kontenjan).NotEmpty();

        }
    }
    public class UpdateDersAcmaValidator : AbstractValidator<UpdateDersAcmaCommand>
    {
        public UpdateDersAcmaValidator()
        {
            RuleFor(x => x.AkademikDonemId).NotEmpty();
            RuleFor(x => x.MufredatId).NotEmpty();
            RuleFor(x => x.OgrElmId).NotEmpty();
            RuleFor(x => x.Kontenjan).NotEmpty();

        }
    }
}