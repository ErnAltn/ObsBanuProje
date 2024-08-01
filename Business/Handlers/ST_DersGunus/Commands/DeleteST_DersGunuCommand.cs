
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


namespace Business.Handlers.ST_DersGunus.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteST_DersGunuCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteST_DersGunuCommandHandler : IRequestHandler<DeleteST_DersGunuCommand, IResult>
        {
            private readonly IST_DersGunuRepository _sT_DersGunuRepository;
            private readonly IMediator _mediator;

            public DeleteST_DersGunuCommandHandler(IST_DersGunuRepository sT_DersGunuRepository, IMediator mediator)
            {
                _sT_DersGunuRepository = sT_DersGunuRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteST_DersGunuCommand request, CancellationToken cancellationToken)
            {
                var sT_DersGunuToDelete = _sT_DersGunuRepository.Get(p => p.Id == request.Id);

                _sT_DersGunuRepository.Delete(sT_DersGunuToDelete);
                await _sT_DersGunuRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

