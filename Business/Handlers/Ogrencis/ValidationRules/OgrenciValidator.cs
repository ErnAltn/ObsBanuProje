
using Business.Handlers.Ogrencis.Commands;
using FluentValidation;

namespace Business.Handlers.Ogrencis.ValidationRules
{

    public class CreateOgrenciValidator : AbstractValidator<CreateOgrenciCommand>
    {
        public CreateOgrenciValidator()
        {
            RuleFor(x => x.OgrenciNo).NotEmpty();
            RuleFor(x => x.DurumId).NotEmpty();
            RuleFor(x => x.Adi).NotEmpty();
            RuleFor(x => x.Soyadi).NotEmpty();
            RuleFor(x => x.TcKimlikNo).NotEmpty();
            RuleFor(x => x.DogumTarihi).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();

        }
    }
    public class UpdateOgrenciValidator : AbstractValidator<UpdateOgrenciCommand>
    {
        public UpdateOgrenciValidator()
        {
            RuleFor(x => x.OgrenciNo).NotEmpty();
            RuleFor(x => x.DurumId).NotEmpty();
            RuleFor(x => x.Adi).NotEmpty();
            RuleFor(x => x.Soyadi).NotEmpty();
            RuleFor(x => x.TcKimlikNo).NotEmpty();
            RuleFor(x => x.DogumTarihi).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();

        }
    }
}