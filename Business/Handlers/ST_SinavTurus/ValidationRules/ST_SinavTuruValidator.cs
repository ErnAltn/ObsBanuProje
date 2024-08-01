
using Business.Handlers.ST_SinavTurus.Commands;
using FluentValidation;

namespace Business.Handlers.ST_SinavTurus.ValidationRules
{

    public class CreateST_SinavTuruValidator : AbstractValidator<CreateST_SinavTuruCommand>
    {
        public CreateST_SinavTuruValidator()
        {
            RuleFor(x => x.Ad).NotEmpty();
            RuleFor(x => x.Ekstra).NotEmpty();

        }
    }
    public class UpdateST_SinavTuruValidator : AbstractValidator<UpdateST_SinavTuruCommand>
    {
        public UpdateST_SinavTuruValidator()
        {
            RuleFor(x => x.Ad).NotEmpty();
            RuleFor(x => x.Ekstra).NotEmpty();

        }
    }
}