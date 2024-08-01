
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

namespace Business.Handlers.ST_OgretimDilis.Queries
{

    public class GetST_OgretimDilisQuery : IRequest<IDataResult<IEnumerable<ST_OgretimDili>>>
    {
        public class GetST_OgretimDilisQueryHandler : IRequestHandler<GetST_OgretimDilisQuery, IDataResult<IEnumerable<ST_OgretimDili>>>
        {
            private readonly IST_OgretimDiliRepository _sT_OgretimDiliRepository;
            private readonly IMediator _mediator;

            public GetST_OgretimDilisQueryHandler(IST_OgretimDiliRepository sT_OgretimDiliRepository, IMediator mediator)
            {
                _sT_OgretimDiliRepository = sT_OgretimDiliRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<ST_OgretimDili>>> Handle(GetST_OgretimDilisQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<ST_OgretimDili>>(await _sT_OgretimDiliRepository.GetListAsync());
            }
        }
    }
}