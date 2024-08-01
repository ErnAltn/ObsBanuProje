
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

namespace Business.Handlers.ST_DersSeviyesis.Queries
{

    public class GetST_DersSeviyesisQuery : IRequest<IDataResult<IEnumerable<ST_DersSeviyesi>>>
    {
        public class GetST_DersSeviyesisQueryHandler : IRequestHandler<GetST_DersSeviyesisQuery, IDataResult<IEnumerable<ST_DersSeviyesi>>>
        {
            private readonly IST_DersSeviyesiRepository _sT_DersSeviyesiRepository;
            private readonly IMediator _mediator;

            public GetST_DersSeviyesisQueryHandler(IST_DersSeviyesiRepository sT_DersSeviyesiRepository, IMediator mediator)
            {
                _sT_DersSeviyesiRepository = sT_DersSeviyesiRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<ST_DersSeviyesi>>> Handle(GetST_DersSeviyesisQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<ST_DersSeviyesi>>(await _sT_DersSeviyesiRepository.GetListAsync());
            }
        }
    }
}