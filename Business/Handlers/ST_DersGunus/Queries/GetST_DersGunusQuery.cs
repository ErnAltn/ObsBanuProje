
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

namespace Business.Handlers.ST_DersGunus.Queries
{

    public class GetST_DersGunusQuery : IRequest<IDataResult<IEnumerable<ST_DersGunu>>>
    {
        public class GetST_DersGunusQueryHandler : IRequestHandler<GetST_DersGunusQuery, IDataResult<IEnumerable<ST_DersGunu>>>
        {
            private readonly IST_DersGunuRepository _sT_DersGunuRepository;
            private readonly IMediator _mediator;

            public GetST_DersGunusQueryHandler(IST_DersGunuRepository sT_DersGunuRepository, IMediator mediator)
            {
                _sT_DersGunuRepository = sT_DersGunuRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<ST_DersGunu>>> Handle(GetST_DersGunusQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<ST_DersGunu>>(await _sT_DersGunuRepository.GetListAsync());
            }
        }
    }
}