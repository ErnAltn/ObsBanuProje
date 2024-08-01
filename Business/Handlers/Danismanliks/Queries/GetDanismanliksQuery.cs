
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

namespace Business.Handlers.Danismanliks.Queries
{

    public class GetDanismanliksQuery : IRequest<IDataResult<IEnumerable<Danismanlik>>>
    {
        public class GetDanismanliksQueryHandler : IRequestHandler<GetDanismanliksQuery, IDataResult<IEnumerable<Danismanlik>>>
        {
            private readonly IDanismanlikRepository _danismanlikRepository;
            private readonly IMediator _mediator;

            public GetDanismanliksQueryHandler(IDanismanlikRepository danismanlikRepository, IMediator mediator)
            {
                _danismanlikRepository = danismanlikRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Danismanlik>>> Handle(GetDanismanliksQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Danismanlik>>(await _danismanlikRepository.GetListAsync());
            }
        }
    }
}