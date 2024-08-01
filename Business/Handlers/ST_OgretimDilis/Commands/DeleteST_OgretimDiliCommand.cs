
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


namespace Business.Handlers.ST_OgretimDilis.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteST_OgretimDiliCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteST_OgretimDiliCommandHandler : IRequestHandler<DeleteST_OgretimDiliCommand, IResult>
        {
            private readonly IST_OgretimDiliRepository _sT_OgretimDiliRepository;
            private readonly IMediator _mediator;

            public DeleteST_OgretimDiliCommandHandler(IST_OgretimDiliRepository sT_OgretimDiliRepository, IMediator mediator)
            {
                _sT_OgretimDiliRepository = sT_OgretimDiliRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteST_OgretimDiliCommand request, CancellationToken cancellationToken)
            {
                var sT_OgretimDiliToDelete = _sT_OgretimDiliRepository.Get(p => p.Id == request.Id);

                _sT_OgretimDiliRepository.Delete(sT_OgretimDiliToDelete);
                await _sT_OgretimDiliRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

