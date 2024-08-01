
using Business.Handlers.Bolums.Commands;
using FluentValidation;

namespace Business.Handlers.Bolums.ValidationRules
{

    public class CreateBolumValidator : AbstractValidator<CreateBolumCommand>
    {
        public CreateBolumValidator()
        {
            RuleFor(x => x.OgretimTuruId).NotEmpty();
            RuleFor(x => x.OgretimDiliId).NotEmpty();
            RuleFor(x => x.BolumAdi).NotEmpty();
            RuleFor(x => x.WebAdresi).NotEmpty();

        }
    }
    public class UpdateBolumValidator : AbstractValidator<UpdateBolumCommand>
    {
        public UpdateBolumValidator()
        {
            RuleFor(x => x.OgretimTuruId).NotEmpty();
            RuleFor(x => x.OgretimDiliId).NotEmpty();
            RuleFor(x => x.BolumAdi).NotEmpty();
            RuleFor(x => x.WebAdresi).NotEmpty();

        }
    }
}