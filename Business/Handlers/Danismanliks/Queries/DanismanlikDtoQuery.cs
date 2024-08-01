using Amazon.Runtime.Internal;
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

namespace Business.Handlers.Danismanliks.Queries
{
    public class DanismanlikDtoQuery: IRequest<IDataResult<IEnumerable<DanismanlikDto>>>
    {

        public class DanismanlikDtoQueryHandler : IRequestHandler<DanismanlikDtoQuery, IDataResult<IEnumerable<DanismanlikDto>>>
        {
            private readonly IDanismanlikRepository _danismanlikRepository;
            private readonly IMediator _mediator;

            public DanismanlikDtoQueryHandler(IDanismanlikRepository danismanlikRepository, IMediator mediator)
            {
                _danismanlikRepository = danismanlikRepository;
                _mediator = mediator;
            }
            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]

            public async Task<IDataResult<IEnumerable<DanismanlikDto>>> Handle(DanismanlikDtoQuery request, CancellationToken cancellationToken)
            {
                var result =  _danismanlikRepository.GetDanismanlikDtoAsync();
                return new SuccessDataResult<IEnumerable<DanismanlikDto>>(result, "DanismanlikDto getirildi!");
            }
        }

    }
}
