
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.ST_SinavTurus.Queries
{
    public class GetST_SinavTuruQuery : IRequest<IDataResult<ST_SinavTuru>>
    {
        public int Id { get; set; }

        public class GetST_SinavTuruQueryHandler : IRequestHandler<GetST_SinavTuruQuery, IDataResult<ST_SinavTuru>>
        {
            private readonly IST_SinavTuruRepository _sT_SinavTuruRepository;
            private readonly IMediator _mediator;

            public GetST_SinavTuruQueryHandler(IST_SinavTuruRepository sT_SinavTuruRepository, IMediator mediator)
            {
                _sT_SinavTuruRepository = sT_SinavTuruRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<ST_SinavTuru>> Handle(GetST_SinavTuruQuery request, CancellationToken cancellationToken)
            {
                var sT_SinavTuru = await _sT_SinavTuruRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<ST_SinavTuru>(sT_SinavTuru);
            }
        }
    }
}
