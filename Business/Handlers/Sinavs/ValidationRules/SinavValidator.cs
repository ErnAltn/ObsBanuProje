
using Business.Handlers.Sinavs.Commands;
using FluentValidation;

namespace Business.Handlers.Sinavs.ValidationRules
{

    public class CreateSinavValidator : AbstractValidator<CreateSinavCommand>
    {
        public CreateSinavValidator()
        {

            RuleFor(x => x.DerslikId).NotEmpty();
            RuleFor(x => x.OgrElmID).NotEmpty();
            RuleFor(x => x.EtkiOrani).NotEmpty();
            RuleFor(x => x.SinavTarihi).NotEmpty();

        }
    }
    public class UpdateSinavValidator : AbstractValidator<UpdateSinavCommand>
    {
        public UpdateSinavValidator()
        {
            RuleFor(x => x.SinavTuruId).NotEmpty();
            RuleFor(x => x.DerslikId).NotEmpty();
            RuleFor(x => x.OgrElmID).NotEmpty();
            RuleFor(x => x.EtkiOrani).NotEmpty();
            RuleFor(x => x.SinavTarihi).NotEmpty();

        }
    }
}