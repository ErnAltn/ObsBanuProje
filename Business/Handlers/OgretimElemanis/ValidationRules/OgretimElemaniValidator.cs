
using Business.Handlers.OgretimElemanis.Commands;
using FluentValidation;

namespace Business.Handlers.OgretimElemanis.ValidationRules
{

    public class CreateOgretimElemaniValidator : AbstractValidator<CreateOgretimElemaniCommand>
    {
        public CreateOgretimElemaniValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.KurumSicilNo).NotEmpty();
            RuleFor(x => x.Unvan).NotEmpty();
            RuleFor(x => x.Adi).NotEmpty();
            RuleFor(x => x.Soyadi).NotEmpty();
            RuleFor(x => x.TCKimlikNo).NotEmpty();
            RuleFor(x => x.Cinsiyet).NotEmpty();
            RuleFor(x => x.DogumTarihi).NotEmpty();

        }
    }
    public class UpdateOgretimElemaniValidator : AbstractValidator<UpdateOgretimElemaniCommand>
    {
        public UpdateOgretimElemaniValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.KurumSicilNo).NotEmpty();
            RuleFor(x => x.Unvan).NotEmpty();
            RuleFor(x => x.Adi).NotEmpty();
            RuleFor(x => x.Soyadi).NotEmpty();
            RuleFor(x => x.TCKimlikNo).NotEmpty();
            RuleFor(x => x.Cinsiyet).NotEmpty();
            RuleFor(x => x.DogumTarihi).NotEmpty();

        }
    }
}