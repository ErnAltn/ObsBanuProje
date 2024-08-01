
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

namespace Business.Handlers.Mufredats.Queries
{

    public class GetMufredatsQuery : IRequest<IDataResult<IEnumerable<Mufredat>>>
    {
        public class GetMufredatsQueryHandler : IRequestHandler<GetMufredatsQuery, IDataResult<IEnumerable<Mufredat>>>
        {
            private readonly IMufredatRepository _mufredatRepository;
            private readonly IMediator _mediator;

            public GetMufredatsQueryHandler(IMufredatRepository mufredatRepository, IMediator mediator)
            {
                _mufredatRepository = mufredatRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Mufredat>>> Handle(GetMufredatsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Mufredat>>(await _mufredatRepository.GetListAsync());
            }
        }
    }
}