
using Business.Handlers.ST_ProgramTurus.Commands;
using FluentValidation;

namespace Business.Handlers.ST_ProgramTurus.ValidationRules
{

    public class CreateST_ProgramTuruValidator : AbstractValidator<CreateST_ProgramTuruCommand>
    {
        public CreateST_ProgramTuruValidator()
        {
            RuleFor(x => x.Ad).NotEmpty();
           

        }
    }
    public class UpdateST_ProgramTuruValidator : AbstractValidator<UpdateST_ProgramTuruCommand>
    {
        public UpdateST_ProgramTuruValidator()
        {
            RuleFor(x => x.Ad).NotEmpty();
           

        }
    }
}