
using Business.Handlers.Danismanliks.Commands;
using FluentValidation;

namespace Business.Handlers.Danismanliks.ValidationRules
{

    public class CreateDanismanlikValidator : AbstractValidator<CreateDanismanlikCommand>
    {
        public CreateDanismanlikValidator()
        {
            RuleFor(x => x.OgrenciId).NotEmpty();

        }
    }
    public class UpdateDanismanlikValidator : AbstractValidator<UpdateDanismanlikCommand>
    {
        public UpdateDanismanlikValidator()
        {
            RuleFor(x => x.OgrenciId).NotEmpty();

        }
    }
}