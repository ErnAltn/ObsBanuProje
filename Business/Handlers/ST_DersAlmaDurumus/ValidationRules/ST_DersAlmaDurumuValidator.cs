
using Business.Handlers.ST_DersAlmaDurumus.Commands;
using FluentValidation;

namespace Business.Handlers.ST_DersAlmaDurumus.ValidationRules
{

    public class CreateST_DersAlmaDurumuValidator : AbstractValidator<CreateST_DersAlmaDurumuCommand>
    {
        public CreateST_DersAlmaDurumuValidator()
        {
            RuleFor(x => x.Ad).NotEmpty();
            RuleFor(x => x.Ekstra).NotEmpty();

        }
    }
    public class UpdateST_DersAlmaDurumuValidator : AbstractValidator<UpdateST_DersAlmaDurumuCommand>
    {
        public UpdateST_DersAlmaDurumuValidator()
        {
            RuleFor(x => x.Ad).NotEmpty();
            RuleFor(x => x.Ekstra).NotEmpty();

        }
    }
}