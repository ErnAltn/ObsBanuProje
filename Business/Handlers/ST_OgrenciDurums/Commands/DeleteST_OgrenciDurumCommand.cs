
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.ST_OgrenciDurums.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteST_OgrenciDurumCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteST_OgrenciDurumCommandHandler : IRequestHandler<DeleteST_OgrenciDurumCommand, IResult>
        {
            private readonly IST_OgrenciDurumRepository _sT_OgrenciDurumRepository;
            private readonly IMediator _mediator;

            public DeleteST_OgrenciDurumCommandHandler(IST_OgrenciDurumRepository sT_OgrenciDurumRepository, IMediator mediator)
            {
                _sT_OgrenciDurumRepository = sT_OgrenciDurumRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteST_OgrenciDurumCommand request, CancellationToken cancellationToken)
            {
                var sT_OgrenciDurumToDelete = _sT_OgrenciDurumRepository.Get(p => p.Id == request.Id);

                _sT_OgrenciDurumRepository.Delete(sT_OgrenciDurumToDelete);
                await _sT_OgrenciDurumRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

