
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

namespace Business.Handlers.ST_DersAlmaDurumus.Queries
{

    public class GetST_DersAlmaDurumusQuery : IRequest<IDataResult<IEnumerable<ST_DersAlmaDurumu>>>
    {
        public class GetST_DersAlmaDurumusQueryHandler : IRequestHandler<GetST_DersAlmaDurumusQuery, IDataResult<IEnumerable<ST_DersAlmaDurumu>>>
        {
            private readonly IST_DersAlmaDurumuRepository _sT_DersAlmaDurumuRepository;
            private readonly IMediator _mediator;

            public GetST_DersAlmaDurumusQueryHandler(IST_DersAlmaDurumuRepository sT_DersAlmaDurumuRepository, IMediator mediator)
            {
                _sT_DersAlmaDurumuRepository = sT_DersAlmaDurumuRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<ST_DersAlmaDurumu>>> Handle(GetST_DersAlmaDurumusQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<ST_DersAlmaDurumu>>(await _sT_DersAlmaDurumuRepository.GetListAsync());
            }
        }
    }
}