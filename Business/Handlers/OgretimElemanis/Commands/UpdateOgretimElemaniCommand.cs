
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.OgretimElemanis.ValidationRules;
using System;

namespace Business.Handlers.OgretimElemanis.Commands
{


    public class UpdateOgretimElemaniCommand : IRequest<IResult>
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

        public class UpdateOgretimElemaniCommandHandler : IRequestHandler<UpdateOgretimElemaniCommand, IResult>
        {
            private readonly IOgretimElemaniRepository _ogretimElemaniRepository;
            private readonly IMediator _mediator;

            public UpdateOgretimElemaniCommandHandler(IOgretimElemaniRepository ogretimElemaniRepository, IMediator mediator)
            {
                _ogretimElemaniRepository = ogretimElemaniRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateOgretimElemaniValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateOgretimElemaniCommand request, CancellationToken cancellationToken)
            {
                var isThereOgretimElemaniRecord = await _ogretimElemaniRepository.GetAsync(u => u.Id == request.Id);

                isThereOgretimElemaniRecord.UpdatedDate = DateTime.Now;
                isThereOgretimElemaniRecord.BolumId = request.BolumId;
                isThereOgretimElemaniRecord.UserId = request.UserId;
                isThereOgretimElemaniRecord.KurumSicilNo = request.KurumSicilNo;
                isThereOgretimElemaniRecord.Unvan = request.Unvan;
                isThereOgretimElemaniRecord.Adi = request.Adi;
                isThereOgretimElemaniRecord.Soyadi = request.Soyadi;
                isThereOgretimElemaniRecord.TCKimlikNo = request.TCKimlikNo;
                isThereOgretimElemaniRecord.Cinsiyet = request.Cinsiyet;
                isThereOgretimElemaniRecord.DogumTarihi = request.DogumTarihi;


                _ogretimElemaniRepository.Update(isThereOgretimElemaniRecord);
                await _ogretimElemaniRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

