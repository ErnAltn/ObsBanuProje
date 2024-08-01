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

namespace Business.Handlers.DersHavuzus.Queries
{
    public class GetDersHavuzuDtoQuery:IRequest<IDataResult<IEnumerable<DersHavuzuDto>>>
    {
       public class GetDersHavuzuDtoQueryHandler : IRequestHandler<GetDersHavuzuDtoQuery, IDataResult<IEnumerable<DersHavuzuDto>>>
        {
            private readonly IDersHavuzuRepository _dersHavuzuRepository;
            private readonly IMediator _mediator;

            public GetDersHavuzuDtoQueryHandler(IDersHavuzuRepository dersHavuzuRepository, IMediator mediator)
            {
                _dersHavuzuRepository = dersHavuzuRepository;
                _mediator = mediator;
            }
            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<DersHavuzuDto>>> Handle(GetDersHavuzuDtoQuery request, CancellationToken cancellationToken)
            {
                var result = _dersHavuzuRepository.GetDersHavuzuDtoAsync();
                return new SuccessDataResult<IEnumerable<DersHavuzuDto>>(result);
            }
        }
    }
}
