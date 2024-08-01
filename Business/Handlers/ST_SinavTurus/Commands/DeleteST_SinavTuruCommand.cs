
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


namespace Business.Handlers.ST_SinavTurus.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteST_SinavTuruCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteST_SinavTuruCommandHandler : IRequestHandler<DeleteST_SinavTuruCommand, IResult>
        {
            private readonly IST_SinavTuruRepository _sT_SinavTuruRepository;
            private readonly IMediator _mediator;

            public DeleteST_SinavTuruCommandHandler(IST_SinavTuruRepository sT_SinavTuruRepository, IMediator mediator)
            {
                _sT_SinavTuruRepository = sT_SinavTuruRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteST_SinavTuruCommand request, CancellationToken cancellationToken)
            {
                var sT_SinavTuruToDelete = _sT_SinavTuruRepository.Get(p => p.Id == request.Id);

                _sT_SinavTuruRepository.Delete(sT_SinavTuruToDelete);
                await _sT_SinavTuruRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

