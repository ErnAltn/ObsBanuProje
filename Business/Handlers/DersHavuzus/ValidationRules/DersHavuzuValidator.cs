
using Business.Handlers.DersHavuzus.Commands;
using FluentValidation;

namespace Business.Handlers.DersHavuzus.ValidationRules
{

    public class CreateDersHavuzuValidator : AbstractValidator<CreateDersHavuzuCommand>
    {
        public CreateDersHavuzuValidator()
        {
            RuleFor(x => x.DersSeviyesiId).NotEmpty();
            RuleFor(x => x.DersturuId).NotEmpty();
            RuleFor(x => x.DersKodu).NotEmpty();
            RuleFor(x => x.DersAdi).NotEmpty();
            RuleFor(x => x.Teorik).NotEmpty();
            RuleFor(x => x.Uygulama).NotEmpty();
            RuleFor(x => x.Kredi).NotEmpty();
            RuleFor(x => x.ECTS).NotEmpty();

        }
    }
    public class UpdateDersHavuzuValidator : AbstractValidator<UpdateDersHavuzuCommand>
    {
        public UpdateDersHavuzuValidator()
        {
            RuleFor(x => x.DersSeviyesiId).NotEmpty();
            RuleFor(x => x.DersturuId).NotEmpty();
            RuleFor(x => x.DersKodu).NotEmpty();
            RuleFor(x => x.DersAdi).NotEmpty();
            RuleFor(x => x.Teorik).NotEmpty();
            RuleFor(x => x.Uygulama).NotEmpty();
            RuleFor(x => x.Kredi).NotEmpty();
            RuleFor(x => x.ECTS).NotEmpty();

        }
    }
}