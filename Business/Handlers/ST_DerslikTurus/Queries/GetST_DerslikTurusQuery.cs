
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

namespace Business.Handlers.ST_DerslikTurus.Queries
{

    public class GetST_DerslikTurusQuery : IRequest<IDataResult<IEnumerable<ST_DerslikTuru>>>
    {
        public class GetST_DerslikTurusQueryHandler : IRequestHandler<GetST_DerslikTurusQuery, IDataResult<IEnumerable<ST_DerslikTuru>>>
        {
            private readonly IST_DerslikTuruRepository _sT_DerslikTuruRepository;
            private readonly IMediator _mediator;

            public GetST_DerslikTurusQueryHandler(IST_DerslikTuruRepository sT_DerslikTuruRepository, IMediator mediator)
            {
                _sT_DerslikTuruRepository = sT_DerslikTuruRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<ST_DerslikTuru>>> Handle(GetST_DerslikTurusQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<ST_DerslikTuru>>(await _sT_DerslikTuruRepository.GetListAsync());
            }
        }
    }
}