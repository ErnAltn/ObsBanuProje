
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.ST_DersDilis.Queries
{
    public class GetST_DersDiliQuery : IRequest<IDataResult<ST_DersDili>>
    {
        public int Id { get; set; }

        public class GetST_DersDiliQueryHandler : IRequestHandler<GetST_DersDiliQuery, IDataResult<ST_DersDili>>
        {
            private readonly IST_DersDiliRepository _sT_DersDiliRepository;
            private readonly IMediator _mediator;

            public GetST_DersDiliQueryHandler(IST_DersDiliRepository sT_DersDiliRepository, IMediator mediator)
            {
                _sT_DersDiliRepository = sT_DersDiliRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<ST_DersDili>> Handle(GetST_DersDiliQuery request, CancellationToken cancellationToken)
            {
                var sT_DersDili = await _sT_DersDiliRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<ST_DersDili>(sT_DersDili);
            }
        }
    }
}
