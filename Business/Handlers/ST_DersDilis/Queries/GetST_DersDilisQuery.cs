
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

namespace Business.Handlers.ST_DersDilis.Queries
{

    public class GetST_DersDilisQuery : IRequest<IDataResult<IEnumerable<ST_DersDili>>>
    {
        public class GetST_DersDilisQueryHandler : IRequestHandler<GetST_DersDilisQuery, IDataResult<IEnumerable<ST_DersDili>>>
        {
            private readonly IST_DersDiliRepository _sT_DersDiliRepository;
            private readonly IMediator _mediator;

            public GetST_DersDilisQueryHandler(IST_DersDiliRepository sT_DersDiliRepository, IMediator mediator)
            {
                _sT_DersDiliRepository = sT_DersDiliRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<ST_DersDili>>> Handle(GetST_DersDilisQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<ST_DersDili>>(await _sT_DersDiliRepository.GetListAsync());
            }
        }
    }
}