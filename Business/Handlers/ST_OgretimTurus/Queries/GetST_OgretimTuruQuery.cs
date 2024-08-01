
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.ST_OgretimTurus.Queries
{
    public class GetST_OgretimTuruQuery : IRequest<IDataResult<ST_OgretimTuru>>
    {
        public int Id { get; set; }

        public class GetST_OgretimTuruQueryHandler : IRequestHandler<GetST_OgretimTuruQuery, IDataResult<ST_OgretimTuru>>
        {
            private readonly IST_OgretimTuruRepository _sT_OgretimTuruRepository;
            private readonly IMediator _mediator;

            public GetST_OgretimTuruQueryHandler(IST_OgretimTuruRepository sT_OgretimTuruRepository, IMediator mediator)
            {
                _sT_OgretimTuruRepository = sT_OgretimTuruRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<ST_OgretimTuru>> Handle(GetST_OgretimTuruQuery request, CancellationToken cancellationToken)
            {
                var sT_OgretimTuru = await _sT_OgretimTuruRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<ST_OgretimTuru>(sT_OgretimTuru);
            }
        }
    }
}
