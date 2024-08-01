
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.ST_DerslikTurus.Queries
{
    public class GetST_DerslikTuruQuery : IRequest<IDataResult<ST_DerslikTuru>>
    {
        public int Id { get; set; }

        public class GetST_DerslikTuruQueryHandler : IRequestHandler<GetST_DerslikTuruQuery, IDataResult<ST_DerslikTuru>>
        {
            private readonly IST_DerslikTuruRepository _sT_DerslikTuruRepository;
            private readonly IMediator _mediator;

            public GetST_DerslikTuruQueryHandler(IST_DerslikTuruRepository sT_DerslikTuruRepository, IMediator mediator)
            {
                _sT_DerslikTuruRepository = sT_DerslikTuruRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<ST_DerslikTuru>> Handle(GetST_DerslikTuruQuery request, CancellationToken cancellationToken)
            {
                var sT_DerslikTuru = await _sT_DerslikTuruRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<ST_DerslikTuru>(sT_DerslikTuru);
            }
        }
    }
}
