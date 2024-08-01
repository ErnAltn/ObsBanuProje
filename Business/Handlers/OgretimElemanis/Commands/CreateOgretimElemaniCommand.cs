
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.OgretimElemanis.ValidationRules;
using System;

namespace Business.Handlers.OgretimElemanis.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateOgretimElemaniCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public System.DateTime DeletedDate { get; set; }
        public int BolumId { get; set; }
        public int UserId { get; set; }
        public string KurumSicilNo { get; set; }
        public string Unvan { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string TCKimlikNo { get; set; }
        public bool Cinsiyet { get; set; }
        public System.DateTime DogumTarihi { get; set; }


        public class CreateOgretimElemaniCommandHandler : IRequestHandler<CreateOgretimElemaniCommand, IResult>
        {
            private readonly IOgretimElemaniRepository _ogretimElemaniRepository;
            private readonly IMediator _mediator;
            public CreateOgretimElemaniCommandHandler(IOgretimElemaniRepository ogretimElemaniRepository, IMediator mediator)
            {
                _ogretimElemaniRepository = ogretimElemaniRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateOgretimElemaniValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateOgretimElemaniCommand request, CancellationToken cancellationToken)
            {
                var isThereOgretimElemaniRecord = _ogretimElemaniRepository.Query().Any(u => u.Id == request.Id);

                if (isThereOgretimElemaniRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedOgretimElemani = new OgretimElemani
                {
                    CreatedDate = DateTime.Now,
                    BolumId = request.BolumId,
                    UserId = request.UserId,
                    KurumSicilNo = request.KurumSicilNo,
                    Unvan = request.Unvan,
                    Adi = request.Adi,
                    Soyadi = request.Soyadi,
                    TCKimlikNo = request.TCKimlikNo,
                    Cinsiyet = request.Cinsiyet,
                    DogumTarihi = request.DogumTarihi,

                };

                _ogretimElemaniRepository.Add(addedOgretimElemani);
                await _ogretimElemaniRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}