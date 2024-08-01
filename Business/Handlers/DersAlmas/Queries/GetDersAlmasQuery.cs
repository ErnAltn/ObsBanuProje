
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

namespace Business.Handlers.DersAlmas.Queries
{

    public class GetDersAlmasQuery : IRequest<IDataResult<IEnumerable<DersAlma>>>
    {
        public class GetDersAlmasQueryHandler : IRequestHandler<GetDersAlmasQuery, IDataResult<IEnumerable<DersAlma>>>
        {
            private readonly IDersAlmaRepository _dersAlmaRepository;
            private readonly IMediator _mediator;

            public GetDersAlmasQueryHandler(IDersAlmaRepository dersAlmaRepository, IMediator mediator)
            {
                _dersAlmaRepository = dersAlmaRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<DersAlma>>> Handle(GetDersAlmasQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<DersAlma>>(await _dersAlmaRepository.GetListAsync());
            }
        }
    }
}