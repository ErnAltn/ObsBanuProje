
using Business.Handlers.ST_AkademikYils.Commands;
using FluentValidation;

namespace Business.Handlers.ST_AkademikYils.ValidationRules
{

    public class CreateST_AkademikYilValidator : AbstractValidator<CreateST_AkademikYilCommand>
    {
        public CreateST_AkademikYilValidator()
        {
            RuleFor(x => x.Ad).NotEmpty();
            RuleFor(x => x.Ekstra).NotEmpty();

        }
    }
    public class UpdateST_AkademikYilValidator : AbstractValidator<UpdateST_AkademikYilCommand>
    {
        public UpdateST_AkademikYilValidator()
        {
            RuleFor(x => x.Ad).NotEmpty();
            RuleFor(x => x.Ekstra).NotEmpty();

        }
    }
}