
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
using Business.Handlers.Bolums.ValidationRules;
using System;

namespace Business.Handlers.Bolums.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateBolumCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public System.DateTime DeletedDate { get; set; }
        public int ProgramTuruId { get; set; }
        public int OgretimTuruId { get; set; }
        public int OgretimDiliId { get; set; }
        public string BolumAdi { get; set; }
        public string WebAdresi { get; set; }


        public class CreateBolumCommandHandler : IRequestHandler<CreateBolumCommand, IResult>
        {
            private readonly IBolumRepository _bolumRepository;
            private readonly IMediator _mediator;
            public CreateBolumCommandHandler(IBolumRepository bolumRepository, IMediator mediator)
            {
                _bolumRepository = bolumRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateBolumValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateBolumCommand request, CancellationToken cancellationToken)
            {
                var isThereBolumRecord = _bolumRepository.Query().Any(u => u.Id == request.Id);

                if (isThereBolumRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedBolum = new Bolum
                {
                    CreatedDate=DateTime.Now,
                    ProgramTuruId = request.ProgramTuruId,
                    OgretimTuruId = request.OgretimTuruId,
                    OgretimDiliId = request.OgretimDiliId,
                    BolumAdi = request.BolumAdi,
                    WebAdresi = request.WebAdresi,

                };

                _bolumRepository.Add(addedBolum);
                await _bolumRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}