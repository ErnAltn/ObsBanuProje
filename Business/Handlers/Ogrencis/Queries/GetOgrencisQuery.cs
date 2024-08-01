
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

namespace Business.Handlers.Ogrencis.Queries
{

    public class GetOgrencisQuery : IRequest<IDataResult<IEnumerable<Ogrenci>>>
    {
        public class GetOgrencisQueryHandler : IRequestHandler<GetOgrencisQuery, IDataResult<IEnumerable<Ogrenci>>>
        {
            private readonly IOgrenciRepository _ogrenciRepository;
            private readonly IMediator _mediator;

            public GetOgrencisQueryHandler(IOgrenciRepository ogrenciRepository, IMediator mediator)
            {
                _ogrenciRepository = ogrenciRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Ogrenci>>> Handle(GetOgrencisQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Ogrenci>>(await _ogrenciRepository.GetListAsync());
            }
        }
    }
}