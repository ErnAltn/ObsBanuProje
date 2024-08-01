
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


namespace Business.Handlers.ST_DersAlmaDurumus.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteST_DersAlmaDurumuCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteST_DersAlmaDurumuCommandHandler : IRequestHandler<DeleteST_DersAlmaDurumuCommand, IResult>
        {
            private readonly IST_DersAlmaDurumuRepository _sT_DersAlmaDurumuRepository;
            private readonly IMediator _mediator;

            public DeleteST_DersAlmaDurumuCommandHandler(IST_DersAlmaDurumuRepository sT_DersAlmaDurumuRepository, IMediator mediator)
            {
                _sT_DersAlmaDurumuRepository = sT_DersAlmaDurumuRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteST_DersAlmaDurumuCommand request, CancellationToken cancellationToken)
            {
                var sT_DersAlmaDurumuToDelete = _sT_DersAlmaDurumuRepository.Get(p => p.Id == request.Id);

                _sT_DersAlmaDurumuRepository.Delete(sT_DersAlmaDurumuToDelete);
                await _sT_DersAlmaDurumuRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

