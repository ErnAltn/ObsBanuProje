
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.ST_OgretimDilis.Queries
{
    public class GetST_OgretimDiliQuery : IRequest<IDataResult<ST_OgretimDili>>
    {
        public int Id { get; set; }

        public class GetST_OgretimDiliQueryHandler : IRequestHandler<GetST_OgretimDiliQuery, IDataResult<ST_OgretimDili>>
        {
            private readonly IST_OgretimDiliRepository _sT_OgretimDiliRepository;
            private readonly IMediator _mediator;

            public GetST_OgretimDiliQueryHandler(IST_OgretimDiliRepository sT_OgretimDiliRepository, IMediator mediator)
            {
                _sT_OgretimDiliRepository = sT_OgretimDiliRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<ST_OgretimDili>> Handle(GetST_OgretimDiliQuery request, CancellationToken cancellationToken)
            {
                var sT_OgretimDili = await _sT_OgretimDiliRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<ST_OgretimDili>(sT_OgretimDili);
            }
        }
    }
}
