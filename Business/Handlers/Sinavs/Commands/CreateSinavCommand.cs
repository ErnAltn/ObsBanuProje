
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
using Business.Handlers.Sinavs.ValidationRules;
using System;

namespace Business.Handlers.Sinavs.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateSinavCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public System.DateTime DeletedDate { get; set; }
        public int DersAcmaId { get; set; }
        public int SinavTuruId { get; set; }
        public int DerslikId { get; set; }
        public int OgrElmID { get; set; }
        public int EtkiOrani { get; set; }
        public System.DateTime SinavTarihi { get; set; }


        public class CreateSinavCommandHandler : IRequestHandler<CreateSinavCommand, IResult>
        {
            private readonly ISinavRepository _sinavRepository;
            private readonly IMediator _mediator;
            public CreateSinavCommandHandler(ISinavRepository sinavRepository, IMediator mediator)
            {
                _sinavRepository = sinavRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateSinavValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateSinavCommand request, CancellationToken cancellationToken)
            {
                var isThereSinavRecord = _sinavRepository.Query().Any(u => u.Id == request.Id);

                if (isThereSinavRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedSinav = new Sinav
                {
                    CreatedDate = DateTime.Now,
                    DersAcmaId = request.DersAcmaId,
                    SinavTuruId = request.SinavTuruId,
                    DerslikId = request.DerslikId,
                    OgrElmID = request.OgrElmID,
                    EtkiOrani = request.EtkiOrani,
                    SinavTarihi = request.SinavTarihi,

                };

                _sinavRepository.Add(addedSinav);
                await _sinavRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}