
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
using Business.Handlers.Bolums.ValidationRules;
using System;

namespace Business.Handlers.Bolums.Commands
{


    public class UpdateBolumCommand : IRequest<IResult>
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

        public class UpdateBolumCommandHandler : IRequestHandler<UpdateBolumCommand, IResult>
        {
            private readonly IBolumRepository _bolumRepository;
            private readonly IMediator _mediator;

            public UpdateBolumCommandHandler(IBolumRepository bolumRepository, IMediator mediator)
            {
                _bolumRepository = bolumRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateBolumValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateBolumCommand request, CancellationToken cancellationToken)
            {
                var isThereBolumRecord = await _bolumRepository.GetAsync(u => u.Id == request.Id);

         
                isThereBolumRecord.UpdatedDate = DateTime.Now;
                isThereBolumRecord.ProgramTuruId = request.ProgramTuruId;
                isThereBolumRecord.OgretimTuruId = request.OgretimTuruId;
                isThereBolumRecord.OgretimDiliId = request.OgretimDiliId;
                isThereBolumRecord.BolumAdi = request.BolumAdi;
                isThereBolumRecord.WebAdresi = request.WebAdresi;


                _bolumRepository.Update(isThereBolumRecord);
                await _bolumRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

