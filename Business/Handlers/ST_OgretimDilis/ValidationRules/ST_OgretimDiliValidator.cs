
using Business.Handlers.ST_OgretimDilis.Commands;
using FluentValidation;

namespace Business.Handlers.ST_OgretimDilis.ValidationRules
{

    public class CreateST_OgretimDiliValidator : AbstractValidator<CreateST_OgretimDiliCommand>
    {
        public CreateST_OgretimDiliValidator()
        {
            RuleFor(x => x.Ad).NotEmpty();
            RuleFor(x => x.Ekstra).NotEmpty();

        }
    }
    public class UpdateST_OgretimDiliValidator : AbstractValidator<UpdateST_OgretimDiliCommand>
    {
        public UpdateST_OgretimDiliValidator()
        {
            RuleFor(x => x.Ad).NotEmpty();
            RuleFor(x => x.Ekstra).NotEmpty();

        }
    }
}