
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.ST_OgrenciDurums.Queries
{
    public class GetST_OgrenciDurumQuery : IRequest<IDataResult<ST_OgrenciDurum>>
    {
        public int Id { get; set; }

        public class GetST_OgrenciDurumQueryHandler : IRequestHandler<GetST_OgrenciDurumQuery, IDataResult<ST_OgrenciDurum>>
        {
            private readonly IST_OgrenciDurumRepository _sT_OgrenciDurumRepository;
            private readonly IMediator _mediator;

            public GetST_OgrenciDurumQueryHandler(IST_OgrenciDurumRepository sT_OgrenciDurumRepository, IMediator mediator)
            {
                _sT_OgrenciDurumRepository = sT_OgrenciDurumRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<ST_OgrenciDurum>> Handle(GetST_OgrenciDurumQuery request, CancellationToken cancellationToken)
            {
                var sT_OgrenciDurum = await _sT_OgrenciDurumRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<ST_OgrenciDurum>(sT_OgrenciDurum);
            }
        }
    }
}
