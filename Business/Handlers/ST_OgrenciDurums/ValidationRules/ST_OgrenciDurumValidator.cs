
using Business.Handlers.ST_OgrenciDurums.Commands;
using FluentValidation;

namespace Business.Handlers.ST_OgrenciDurums.ValidationRules
{

    public class CreateST_OgrenciDurumValidator : AbstractValidator<CreateST_OgrenciDurumCommand>
    {
        public CreateST_OgrenciDurumValidator()
        {
            RuleFor(x => x.Ad).NotEmpty();
            RuleFor(x => x.Ekstra).NotEmpty();

        }
    }
    public class UpdateST_OgrenciDurumValidator : AbstractValidator<UpdateST_OgrenciDurumCommand>
    {
        public UpdateST_OgrenciDurumValidator()
        {
            RuleFor(x => x.Ad).NotEmpty();
            RuleFor(x => x.Ekstra).NotEmpty();

        }
    }
}