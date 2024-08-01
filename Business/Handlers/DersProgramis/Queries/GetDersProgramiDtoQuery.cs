using Business.BusinessAspects;
using Business.Handlers.Bolums.Queries;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.DersProgramis.Queries
{
    public class GetDersProgramiDtoQuery: IRequest<IDataResult<IEnumerable<DersProgramiDto>>>
    {
        public class GetDersProgramiDtoQueryHandler : IRequestHandler<GetDersProgramiDtoQuery, IDataResult<IEnumerable<DersProgramiDto>>>
        {
            private readonly IDersProgramiRepository _dersProgramiRepository;
            private readonly IMediator _mediator;


            public GetDersProgramiDtoQueryHandler(IDersProgramiRepository dersProgramiRepository, IMediator mediator)
            {
                _dersProgramiRepository = dersProgramiRepository;
                _mediator = mediator;

            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            

            public async Task<IDataResult<IEnumerable<DersProgramiDto>>> Handle(GetDersProgramiDtoQuery request, CancellationToken cancellationToken)
            {
                var result = _dersProgramiRepository.GetDersProgramiDto();
                return new SuccessDataResult<IEnumerable<DersProgramiDto>>(result);
            }
        }
    }
}
