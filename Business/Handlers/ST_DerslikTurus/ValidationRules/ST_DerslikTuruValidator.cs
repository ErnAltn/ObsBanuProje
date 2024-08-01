
using Business.Handlers.ST_DerslikTurus.Commands;
using FluentValidation;

namespace Business.Handlers.ST_DerslikTurus.ValidationRules
{

    public class CreateST_DerslikTuruValidator : AbstractValidator<CreateST_DerslikTuruCommand>
    {
        public CreateST_DerslikTuruValidator()
        {
            RuleFor(x => x.Ad).NotEmpty();
            RuleFor(x => x.Ekstra).NotEmpty();

        }
    }
    public class UpdateST_DerslikTuruValidator : AbstractValidator<UpdateST_DerslikTuruCommand>
    {
        public UpdateST_DerslikTuruValidator()
        {
            RuleFor(x => x.Ad).NotEmpty();
            RuleFor(x => x.Ekstra).NotEmpty();

        }
    }
}