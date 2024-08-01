
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


namespace Business.Handlers.ST_OgretimTurus.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteST_OgretimTuruCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteST_OgretimTuruCommandHandler : IRequestHandler<DeleteST_OgretimTuruCommand, IResult>
        {
            private readonly IST_OgretimTuruRepository _sT_OgretimTuruRepository;
            private readonly IMediator _mediator;

            public DeleteST_OgretimTuruCommandHandler(IST_OgretimTuruRepository sT_OgretimTuruRepository, IMediator mediator)
            {
                _sT_OgretimTuruRepository = sT_OgretimTuruRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteST_OgretimTuruCommand request, CancellationToken cancellationToken)
            {
                var sT_OgretimTuruToDelete = _sT_OgretimTuruRepository.Get(p => p.Id == request.Id);

                _sT_OgretimTuruRepository.Delete(sT_OgretimTuruToDelete);
                await _sT_OgretimTuruRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

