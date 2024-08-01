
using Business.Handlers.ST_AkademikDonems.Commands;
using FluentValidation;

namespace Business.Handlers.ST_AkademikDonems.ValidationRules
{

    public class CreateST_AkademikDonemValidator : AbstractValidator<CreateST_AkademikDonemCommand>
    {
        public CreateST_AkademikDonemValidator()
        {
            RuleFor(x => x.Ad).NotEmpty();
            RuleFor(x => x.Ekstra).NotEmpty();

        }
    }
    public class UpdateST_AkademikDonemValidator : AbstractValidator<UpdateST_AkademikDonemCommand>
    {
        public UpdateST_AkademikDonemValidator()
        {
            RuleFor(x => x.Ad).NotEmpty();
            RuleFor(x => x.Ekstra).NotEmpty();

        }
    }
}