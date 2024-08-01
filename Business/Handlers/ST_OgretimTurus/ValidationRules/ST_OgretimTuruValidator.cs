
using Business.Handlers.ST_OgretimTurus.Commands;
using FluentValidation;

namespace Business.Handlers.ST_OgretimTurus.ValidationRules
{

    public class CreateST_OgretimTuruValidator : AbstractValidator<CreateST_OgretimTuruCommand>
    {
        public CreateST_OgretimTuruValidator()
        {
            RuleFor(x => x.Ad).NotEmpty();
            RuleFor(x => x.Ekstra).NotEmpty();

        }
    }
    public class UpdateST_OgretimTuruValidator : AbstractValidator<UpdateST_OgretimTuruCommand>
    {
        public UpdateST_OgretimTuruValidator()
        {
            RuleFor(x => x.Ad).NotEmpty();
            RuleFor(x => x.Ekstra).NotEmpty();

        }
    }
}