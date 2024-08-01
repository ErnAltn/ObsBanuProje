
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
using Business.Handlers.Ogrencis.ValidationRules;
using System;
using ServiceStack;

namespace Business.Handlers.Ogrencis.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateOgrenciCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public System.DateTime DeletedDate { get; set; }
        public int BolumId { get; set; }
        public string OgrenciNo { get; set; }
        public int DurumId { get; set; }
        public System.DateTime AyrilmaTarihi { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string TcKimlikNo { get; set; }
        public bool Cinsiyet { get; set; }
        public DateTime DogumTarihi { get; set; }
        public int UserId { get; set; }


        public class CreateOgrenciCommandHandler : IRequestHandler<CreateOgrenciCommand, IResult>
        {
            private readonly IOgrenciRepository _ogrenciRepository;
            private readonly IMediator _mediator;
            public CreateOgrenciCommandHandler(IOgrenciRepository ogrenciRepository, IMediator mediator)
            {
                _ogrenciRepository = ogrenciRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateOgrenciValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateOgrenciCommand request, CancellationToken cancellationToken)
            {
                var isThereOgrenciRecord = _ogrenciRepository.Query().Any(u => u.OgrenciNo == request.OgrenciNo && u.TcKimlikNo==request.TcKimlikNo);

                if (isThereOgrenciRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

             

                var addedOgrenci = new Ogrenci
                {
                    CreatedDate = DateTime.Now,
                    BolumId = request.BolumId,
                    OgrenciNo = request.OgrenciNo,
                    DurumId = request.DurumId,
                    Adi = request.Adi,
                    Soyadi = request.Soyadi,
                    TcKimlikNo = request.TcKimlikNo,
                    Cinsiyet = request.Cinsiyet,
                    DogumTarihi = request.DogumTarihi,
                    UserId = request.UserId,
                };

                _ogrenciRepository.Add(addedOgrenci);
                await _ogrenciRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}