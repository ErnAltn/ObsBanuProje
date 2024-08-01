
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
using Business.Handlers.Degerlendirmes.ValidationRules;
using System;

namespace Business.Handlers.Degerlendirmes.Commands
{


    public class UpdateDegerlendirmeCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public System.DateTime DeletedDate { get; set; }
        public int SinavId { get; set; }
        public int OgrenciId { get; set; }
        public float SinavNotu { get; set; }

        public class UpdateDegerlendirmeCommandHandler : IRequestHandler<UpdateDegerlendirmeCommand, IResult>
        {
            private readonly IDegerlendirmeRepository _degerlendirmeRepository;
            private readonly IMediator _mediator;

            public UpdateDegerlendirmeCommandHandler(IDegerlendirmeRepository degerlendirmeRepository, IMediator mediator)
            {
                _degerlendirmeRepository = degerlendirmeRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateDegerlendirmeValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateDegerlendirmeCommand request, CancellationToken cancellationToken)
            {
                var isThereDegerlendirmeRecord = await _degerlendirmeRepository.GetAsync(u => u.Id == request.Id);



                isThereDegerlendirmeRecord.UpdatedDate = DateTime.Now;
                isThereDegerlendirmeRecord.SinavId = request.SinavId;
                isThereDegerlendirmeRecord.OgrenciId = request.OgrenciId;
                isThereDegerlendirmeRecord.SinavNotu = request.SinavNotu;


                _degerlendirmeRepository.Update(isThereDegerlendirmeRecord);
                await _degerlendirmeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

