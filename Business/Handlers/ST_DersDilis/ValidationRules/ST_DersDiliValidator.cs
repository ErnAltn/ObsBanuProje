
using Business.Handlers.ST_DersDilis.Commands;
using FluentValidation;

namespace Business.Handlers.ST_DersDilis.ValidationRules
{

    public class CreateST_DersDiliValidator : AbstractValidator<CreateST_DersDiliCommand>
    {
        public CreateST_DersDiliValidator()
        {
            RuleFor(x => x.Ad).NotEmpty();
            RuleFor(x => x.Ekstra).NotEmpty();

        }
    }
    public class UpdateST_DersDiliValidator : AbstractValidator<UpdateST_DersDiliCommand>
    {
        public UpdateST_DersDiliValidator()
        {
            RuleFor(x => x.Ad).NotEmpty();
            RuleFor(x => x.Ekstra).NotEmpty();

        }
    }
}