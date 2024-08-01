
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.ST_DersTurus.Queries
{
    public class GetST_DersTuruQuery : IRequest<IDataResult<ST_DersTuru>>
    {
        public int Id { get; set; }

        public class GetST_DersTuruQueryHandler : IRequestHandler<GetST_DersTuruQuery, IDataResult<ST_DersTuru>>
        {
            private readonly IST_DersTuruRepository _sT_DersTuruRepository;
            private readonly IMediator _mediator;

            public GetST_DersTuruQueryHandler(IST_DersTuruRepository sT_DersTuruRepository, IMediator mediator)
            {
                _sT_DersTuruRepository = sT_DersTuruRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<ST_DersTuru>> Handle(GetST_DersTuruQuery request, CancellationToken cancellationToken)
            {
                var sT_DersTuru = await _sT_DersTuruRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<ST_DersTuru>(sT_DersTuru);
            }
        }
    }
}
