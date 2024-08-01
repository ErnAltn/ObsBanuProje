
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


namespace Business.Handlers.ST_DersDilis.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteST_DersDiliCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteST_DersDiliCommandHandler : IRequestHandler<DeleteST_DersDiliCommand, IResult>
        {
            private readonly IST_DersDiliRepository _sT_DersDiliRepository;
            private readonly IMediator _mediator;

            public DeleteST_DersDiliCommandHandler(IST_DersDiliRepository sT_DersDiliRepository, IMediator mediator)
            {
                _sT_DersDiliRepository = sT_DersDiliRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteST_DersDiliCommand request, CancellationToken cancellationToken)
            {
                var sT_DersDiliToDelete = _sT_DersDiliRepository.Get(p => p.Id == request.Id);

                _sT_DersDiliRepository.Delete(sT_DersDiliToDelete);
                await _sT_DersDiliRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

