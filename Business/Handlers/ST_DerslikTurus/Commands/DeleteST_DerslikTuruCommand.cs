
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


namespace Business.Handlers.ST_DerslikTurus.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteST_DerslikTuruCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteST_DerslikTuruCommandHandler : IRequestHandler<DeleteST_DerslikTuruCommand, IResult>
        {
            private readonly IST_DerslikTuruRepository _sT_DerslikTuruRepository;
            private readonly IMediator _mediator;

            public DeleteST_DerslikTuruCommandHandler(IST_DerslikTuruRepository sT_DerslikTuruRepository, IMediator mediator)
            {
                _sT_DerslikTuruRepository = sT_DerslikTuruRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteST_DerslikTuruCommand request, CancellationToken cancellationToken)
            {
                var sT_DerslikTuruToDelete = _sT_DerslikTuruRepository.Get(p => p.Id == request.Id);

                _sT_DerslikTuruRepository.Delete(sT_DerslikTuruToDelete);
                await _sT_DerslikTuruRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

