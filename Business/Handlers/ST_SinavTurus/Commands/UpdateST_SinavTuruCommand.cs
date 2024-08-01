
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
using Business.Handlers.ST_SinavTurus.ValidationRules;


namespace Business.Handlers.ST_SinavTurus.Commands
{


    public class UpdateST_SinavTuruCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Ekstra { get; set; }

        public class UpdateST_SinavTuruCommandHandler : IRequestHandler<UpdateST_SinavTuruCommand, IResult>
        {
            private readonly IST_SinavTuruRepository _sT_SinavTuruRepository;
            private readonly IMediator _mediator;

            public UpdateST_SinavTuruCommandHandler(IST_SinavTuruRepository sT_SinavTuruRepository, IMediator mediator)
            {
                _sT_SinavTuruRepository = sT_SinavTuruRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateST_SinavTuruValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateST_SinavTuruCommand request, CancellationToken cancellationToken)
            {
                var isThereST_SinavTuruRecord = await _sT_SinavTuruRepository.GetAsync(u => u.Id == request.Id);


                isThereST_SinavTuruRecord.Ad = request.Ad;
                isThereST_SinavTuruRecord.Ekstra = request.Ekstra;


                _sT_SinavTuruRepository.Update(isThereST_SinavTuruRecord);
                await _sT_SinavTuruRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

