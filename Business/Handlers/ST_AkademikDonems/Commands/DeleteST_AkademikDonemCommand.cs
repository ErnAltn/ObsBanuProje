
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


namespace Business.Handlers.ST_AkademikDonems.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteST_AkademikDonemCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteST_AkademikDonemCommandHandler : IRequestHandler<DeleteST_AkademikDonemCommand, IResult>
        {
            private readonly IST_AkademikDonemRepository _sT_AkademikDonemRepository;
            private readonly IMediator _mediator;

            public DeleteST_AkademikDonemCommandHandler(IST_AkademikDonemRepository sT_AkademikDonemRepository, IMediator mediator)
            {
                _sT_AkademikDonemRepository = sT_AkademikDonemRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteST_AkademikDonemCommand request, CancellationToken cancellationToken)
            {
                var sT_AkademikDonemToDelete = _sT_AkademikDonemRepository.Get(p => p.Id == request.Id);

                _sT_AkademikDonemRepository.Delete(sT_AkademikDonemToDelete);
                await _sT_AkademikDonemRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

