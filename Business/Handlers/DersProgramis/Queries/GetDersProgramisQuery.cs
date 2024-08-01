
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

namespace Business.Handlers.DersProgramis.Queries
{

    public class GetDersProgramisQuery : IRequest<IDataResult<IEnumerable<DersProgrami>>>
    {
        public class GetDersProgramisQueryHandler : IRequestHandler<GetDersProgramisQuery, IDataResult<IEnumerable<DersProgrami>>>
        {
            private readonly IDersProgramiRepository _dersProgramiRepository;
            private readonly IMediator _mediator;

            public GetDersProgramisQueryHandler(IDersProgramiRepository dersProgramiRepository, IMediator mediator)
            {
                _dersProgramiRepository = dersProgramiRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<DersProgrami>>> Handle(GetDersProgramisQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<DersProgrami>>(await _dersProgramiRepository.GetListAsync());
            }
        }
    }
}