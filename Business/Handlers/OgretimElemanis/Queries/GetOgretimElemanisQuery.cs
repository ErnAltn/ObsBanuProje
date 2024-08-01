
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

namespace Business.Handlers.OgretimElemanis.Queries
{

    public class GetOgretimElemanisQuery : IRequest<IDataResult<IEnumerable<OgretimElemani>>>
    {
        public class GetOgretimElemanisQueryHandler : IRequestHandler<GetOgretimElemanisQuery, IDataResult<IEnumerable<OgretimElemani>>>
        {
            private readonly IOgretimElemaniRepository _ogretimElemaniRepository;
            private readonly IMediator _mediator;

            public GetOgretimElemanisQueryHandler(IOgretimElemaniRepository ogretimElemaniRepository, IMediator mediator)
            {
                _ogretimElemaniRepository = ogretimElemaniRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<OgretimElemani>>> Handle(GetOgretimElemanisQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<OgretimElemani>>(await _ogretimElemaniRepository.GetListAsync());
            }
        }
    }
}