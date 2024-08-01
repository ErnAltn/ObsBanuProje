
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

namespace Business.Handlers.ST_ProgramTurus.Queries
{

    public class GetST_ProgramTurusQuery : IRequest<IDataResult<IEnumerable<ST_ProgramTuru>>>
    {
        public class GetST_ProgramTurusQueryHandler : IRequestHandler<GetST_ProgramTurusQuery, IDataResult<IEnumerable<ST_ProgramTuru>>>
        {
            private readonly IST_ProgramTuruRepository _sT_ProgramTuruRepository;
            private readonly IMediator _mediator;

            public GetST_ProgramTurusQueryHandler(IST_ProgramTuruRepository sT_ProgramTuruRepository, IMediator mediator)
            {
                _sT_ProgramTuruRepository = sT_ProgramTuruRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<ST_ProgramTuru>>> Handle(GetST_ProgramTurusQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<ST_ProgramTuru>>(await _sT_ProgramTuruRepository.GetListAsync());
            }
        }
    }
}