
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

namespace Business.Handlers.ST_OgrenciDurums.Queries
{

    public class GetST_OgrenciDurumsQuery : IRequest<IDataResult<IEnumerable<ST_OgrenciDurum>>>
    {
        public class GetST_OgrenciDurumsQueryHandler : IRequestHandler<GetST_OgrenciDurumsQuery, IDataResult<IEnumerable<ST_OgrenciDurum>>>
        {
            private readonly IST_OgrenciDurumRepository _sT_OgrenciDurumRepository;
            private readonly IMediator _mediator;

            public GetST_OgrenciDurumsQueryHandler(IST_OgrenciDurumRepository sT_OgrenciDurumRepository, IMediator mediator)
            {
                _sT_OgrenciDurumRepository = sT_OgrenciDurumRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<ST_OgrenciDurum>>> Handle(GetST_OgrenciDurumsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<ST_OgrenciDurum>>(await _sT_OgrenciDurumRepository.GetListAsync());
            }
        }
    }
}