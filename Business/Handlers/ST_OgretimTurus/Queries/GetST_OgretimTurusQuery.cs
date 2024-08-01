
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;

namespace Business.Handlers.ST_OgretimTurus.Queries
{

    public class GetST_OgretimTurusQuery : IRequest<IDataResult<IEnumerable<ST_OgretimTuru>>>
    {
        public class GetST_OgretimTurusQueryHandler : IRequestHandler<GetST_OgretimTurusQuery, IDataResult<IEnumerable<ST_OgretimTuru>>>
        {
            private readonly IST_OgretimTuruRepository _sT_OgretimTuruRepository;
            private readonly IMediator _mediator;

            public GetST_OgretimTurusQueryHandler(IST_OgretimTuruRepository sT_OgretimTuruRepository, IMediator mediator)
            {
                _sT_OgretimTuruRepository = sT_OgretimTuruRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<ST_OgretimTuru>>> Handle(GetST_OgretimTurusQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<ST_OgretimTuru>>(await _sT_OgretimTuruRepository.GetListAsync());
            }
        }
    }
}