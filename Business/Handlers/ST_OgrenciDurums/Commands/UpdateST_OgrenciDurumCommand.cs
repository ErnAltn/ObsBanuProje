
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
using Business.Handlers.ST_OgrenciDurums.ValidationRules;


namespace Business.Handlers.ST_OgrenciDurums.Commands
{


    public class UpdateST_OgrenciDurumCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Ekstra { get; set; }

        public class UpdateST_OgrenciDurumCommandHandler : IRequestHandler<UpdateST_OgrenciDurumCommand, IResult>
        {
            private readonly IST_OgrenciDurumRepository _sT_OgrenciDurumRepository;
            private readonly IMediator _mediator;

            public UpdateST_OgrenciDurumCommandHandler(IST_OgrenciDurumRepository sT_OgrenciDurumRepository, IMediator mediator)
            {
                _sT_OgrenciDurumRepository = sT_OgrenciDurumRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateST_OgrenciDurumValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateST_OgrenciDurumCommand request, CancellationToken cancellationToken)
            {
                var isThereST_OgrenciDurumRecord = await _sT_OgrenciDurumRepository.GetAsync(u => u.Id == request.Id);


                isThereST_OgrenciDurumRecord.Ad = request.Ad;
                isThereST_OgrenciDurumRecord.Ekstra = request.Ekstra;


                _sT_OgrenciDurumRepository.Update(isThereST_OgrenciDurumRecord);
                await _sT_OgrenciDurumRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

