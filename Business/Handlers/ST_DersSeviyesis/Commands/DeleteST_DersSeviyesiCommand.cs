
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


namespace Business.Handlers.ST_DersSeviyesis.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteST_DersSeviyesiCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteST_DersSeviyesiCommandHandler : IRequestHandler<DeleteST_DersSeviyesiCommand, IResult>
        {
            private readonly IST_DersSeviyesiRepository _sT_DersSeviyesiRepository;
            private readonly IMediator _mediator;

            public DeleteST_DersSeviyesiCommandHandler(IST_DersSeviyesiRepository sT_DersSeviyesiRepository, IMediator mediator)
            {
                _sT_DersSeviyesiRepository = sT_DersSeviyesiRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteST_DersSeviyesiCommand request, CancellationToken cancellationToken)
            {
                var sT_DersSeviyesiToDelete = _sT_DersSeviyesiRepository.Get(p => p.Id == request.Id);

                _sT_DersSeviyesiRepository.Delete(sT_DersSeviyesiToDelete);
                await _sT_DersSeviyesiRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

