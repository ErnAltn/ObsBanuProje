
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

namespace Business.Handlers.DersHavuzus.Queries
{

    public class GetDersHavuzusQuery : IRequest<IDataResult<IEnumerable<DersHavuzu>>>
    {
        public class GetDersHavuzusQueryHandler : IRequestHandler<GetDersHavuzusQuery, IDataResult<IEnumerable<DersHavuzu>>>
        {
            private readonly IDersHavuzuRepository _dersHavuzuRepository;
            private readonly IMediator _mediator;

            public GetDersHavuzusQueryHandler(IDersHavuzuRepository dersHavuzuRepository, IMediator mediator)
            {
                _dersHavuzuRepository = dersHavuzuRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<DersHavuzu>>> Handle(GetDersHavuzusQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<DersHavuzu>>(await _dersHavuzuRepository.GetListAsync());
            }
        }
    }
}