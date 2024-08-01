
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

namespace Business.Handlers.ST_DersTurus.Queries
{

    public class GetST_DersTurusQuery : IRequest<IDataResult<IEnumerable<ST_DersTuru>>>
    {
        public class GetST_DersTurusQueryHandler : IRequestHandler<GetST_DersTurusQuery, IDataResult<IEnumerable<ST_DersTuru>>>
        {
            private readonly IST_DersTuruRepository _sT_DersTuruRepository;
            private readonly IMediator _mediator;

            public GetST_DersTurusQueryHandler(IST_DersTuruRepository sT_DersTuruRepository, IMediator mediator)
            {
                _sT_DersTuruRepository = sT_DersTuruRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<ST_DersTuru>>> Handle(GetST_DersTurusQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<ST_DersTuru>>(await _sT_DersTuruRepository.GetListAsync());
            }
        }
    }
}