
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

namespace Business.Handlers.ST_AkademikYils.Queries
{

    public class GetST_AkademikYilsQuery : IRequest<IDataResult<IEnumerable<ST_AkademikYil>>>
    {
        public class GetST_AkademikYilsQueryHandler : IRequestHandler<GetST_AkademikYilsQuery, IDataResult<IEnumerable<ST_AkademikYil>>>
        {
            private readonly IST_AkademikYilRepository _sT_AkademikYilRepository;
            private readonly IMediator _mediator;

            public GetST_AkademikYilsQueryHandler(IST_AkademikYilRepository sT_AkademikYilRepository, IMediator mediator)
            {
                _sT_AkademikYilRepository = sT_AkademikYilRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<ST_AkademikYil>>> Handle(GetST_AkademikYilsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<ST_AkademikYil>>(await _sT_AkademikYilRepository.GetListAsync());
            }
        }
    }
}