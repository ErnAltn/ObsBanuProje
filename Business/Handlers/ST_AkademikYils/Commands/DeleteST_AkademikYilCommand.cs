
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


namespace Business.Handlers.ST_AkademikYils.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteST_AkademikYilCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteST_AkademikYilCommandHandler : IRequestHandler<DeleteST_AkademikYilCommand, IResult>
        {
            private readonly IST_AkademikYilRepository _sT_AkademikYilRepository;
            private readonly IMediator _mediator;

            public DeleteST_AkademikYilCommandHandler(IST_AkademikYilRepository sT_AkademikYilRepository, IMediator mediator)
            {
                _sT_AkademikYilRepository = sT_AkademikYilRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteST_AkademikYilCommand request, CancellationToken cancellationToken)
            {
                var sT_AkademikYilToDelete = _sT_AkademikYilRepository.Get(p => p.Id == request.Id);

                _sT_AkademikYilRepository.Delete(sT_AkademikYilToDelete);
                await _sT_AkademikYilRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

