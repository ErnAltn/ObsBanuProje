
using Business.Handlers.ST_DersSeviyesis.Commands;
using FluentValidation;

namespace Business.Handlers.ST_DersSeviyesis.ValidationRules
{

    public class CreateST_DersSeviyesiValidator : AbstractValidator<CreateST_DersSeviyesiCommand>
    {
        public CreateST_DersSeviyesiValidator()
        {
            RuleFor(x => x.Ad).NotEmpty();
            RuleFor(x => x.Ekstra).NotEmpty();

        }
    }
    public class UpdateST_DersSeviyesiValidator : AbstractValidator<UpdateST_DersSeviyesiCommand>
    {
        public UpdateST_DersSeviyesiValidator()
        {
            RuleFor(x => x.Ad).NotEmpty();
            RuleFor(x => x.Ekstra).NotEmpty();

        }
    }
}