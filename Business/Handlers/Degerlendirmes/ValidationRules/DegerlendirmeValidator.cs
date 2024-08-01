
using Business.Handlers.Degerlendirmes.Commands;
using FluentValidation;

namespace Business.Handlers.Degerlendirmes.ValidationRules
{

    public class CreateDegerlendirmeValidator : AbstractValidator<CreateDegerlendirmeCommand>
    {
        public CreateDegerlendirmeValidator()
        {
            RuleFor(x => x.OgrenciId).NotEmpty();
            RuleFor(x => x.SinavNotu).NotEmpty();

        }
    }
    public class UpdateDegerlendirmeValidator : AbstractValidator<UpdateDegerlendirmeCommand>
    {
        public UpdateDegerlendirmeValidator()
        {
            RuleFor(x => x.OgrenciId).NotEmpty();
            RuleFor(x => x.SinavNotu).NotEmpty();

        }
    }
}