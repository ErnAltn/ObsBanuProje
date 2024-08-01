
using Business.Handlers.ST_DersTurus.Commands;
using FluentValidation;

namespace Business.Handlers.ST_DersTurus.ValidationRules
{

    public class CreateST_DersTuruValidator : AbstractValidator<CreateST_DersTuruCommand>
    {
        public CreateST_DersTuruValidator()
        {
            RuleFor(x => x.Ad).NotEmpty();
            RuleFor(x => x.Ekstra).NotEmpty();

        }
    }
    public class UpdateST_DersTuruValidator : AbstractValidator<UpdateST_DersTuruCommand>
    {
        public UpdateST_DersTuruValidator()
        {
            RuleFor(x => x.Ad).NotEmpty();
            RuleFor(x => x.Ekstra).NotEmpty();

        }
    }
}