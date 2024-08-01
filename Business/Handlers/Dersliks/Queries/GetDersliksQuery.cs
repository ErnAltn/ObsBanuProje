
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

namespace Business.Handlers.Dersliks.Queries
{

    public class GetDersliksQuery : IRequest<IDataResult<IEnumerable<Derslik>>>
    {
        public class GetDersliksQueryHandler : IRequestHandler<GetDersliksQuery, IDataResult<IEnumerable<Derslik>>>
        {
            private readonly IDerslikRepository _derslikRepository;
            private readonly IMediator _mediator;

            public GetDersliksQueryHandler(IDerslikRepository derslikRepository, IMediator mediator)
            {
                _derslikRepository = derslikRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Derslik>>> Handle(GetDersliksQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Derslik>>(await _derslikRepository.GetListAsync());
            }
        }
    }
}