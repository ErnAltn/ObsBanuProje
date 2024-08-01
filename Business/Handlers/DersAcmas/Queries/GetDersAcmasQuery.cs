
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

namespace Business.Handlers.DersAcmas.Queries
{

    public class GetDersAcmasQuery : IRequest<IDataResult<IEnumerable<DersAcma>>>
    {
        public class GetDersAcmasQueryHandler : IRequestHandler<GetDersAcmasQuery, IDataResult<IEnumerable<DersAcma>>>
        {
            private readonly IDersAcmaRepository _dersAcmaRepository;
            private readonly IMediator _mediator;

            public GetDersAcmasQueryHandler(IDersAcmaRepository dersAcmaRepository, IMediator mediator)
            {
                _dersAcmaRepository = dersAcmaRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<DersAcma>>> Handle(GetDersAcmasQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<DersAcma>>(await _dersAcmaRepository.GetListAsync());
            }
        }
    }
}