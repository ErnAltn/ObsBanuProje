
using Business.Handlers.ST_DersGunus.Commands;
using FluentValidation;

namespace Business.Handlers.ST_DersGunus.ValidationRules
{

    public class CreateST_DersGunuValidator : AbstractValidator<CreateST_DersGunuCommand>
    {
        public CreateST_DersGunuValidator()
        {
            RuleFor(x => x.Ad).NotEmpty();
            RuleFor(x => x.Ekstra).NotEmpty();

        }
    }
    public class UpdateST_DersGunuValidator : AbstractValidator<UpdateST_DersGunuCommand>
    {
        public UpdateST_DersGunuValidator()
        {
            RuleFor(x => x.Ad).NotEmpty();
            RuleFor(x => x.Ekstra).NotEmpty();

        }
    }
}