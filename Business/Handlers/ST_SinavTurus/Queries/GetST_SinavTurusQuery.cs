
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

namespace Business.Handlers.ST_SinavTurus.Queries
{

    public class GetST_SinavTurusQuery : IRequest<IDataResult<IEnumerable<ST_SinavTuru>>>
    {
        public class GetST_SinavTurusQueryHandler : IRequestHandler<GetST_SinavTurusQuery, IDataResult<IEnumerable<ST_SinavTuru>>>
        {
            private readonly IST_SinavTuruRepository _sT_SinavTuruRepository;
            private readonly IMediator _mediator;

            public GetST_SinavTurusQueryHandler(IST_SinavTuruRepository sT_SinavTuruRepository, IMediator mediator)
            {
                _sT_SinavTuruRepository = sT_SinavTuruRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<ST_SinavTuru>>> Handle(GetST_SinavTurusQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<ST_SinavTuru>>(await _sT_SinavTuruRepository.GetListAsync());
            }
        }
    }
}