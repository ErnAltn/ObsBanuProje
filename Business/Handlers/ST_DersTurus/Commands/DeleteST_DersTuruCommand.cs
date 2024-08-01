
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


namespace Business.Handlers.ST_DersTurus.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteST_DersTuruCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteST_DersTuruCommandHandler : IRequestHandler<DeleteST_DersTuruCommand, IResult>
        {
            private readonly IST_DersTuruRepository _sT_DersTuruRepository;
            private readonly IMediator _mediator;

            public DeleteST_DersTuruCommandHandler(IST_DersTuruRepository sT_DersTuruRepository, IMediator mediator)
            {
                _sT_DersTuruRepository = sT_DersTuruRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteST_DersTuruCommand request, CancellationToken cancellationToken)
            {
                var sT_DersTuruToDelete = _sT_DersTuruRepository.Get(p => p.Id == request.Id);

                _sT_DersTuruRepository.Delete(sT_DersTuruToDelete);
                await _sT_DersTuruRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

