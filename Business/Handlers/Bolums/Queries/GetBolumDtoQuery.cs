using Business.BusinessAspects;
using Business.Handlers.Ogrencis.Queries;
using Business.Services.UserService.Abstract;
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

namespace Business.Handlers.Bolums.Queries
{
    public class GetBolumDtoQuery : IRequest<IDataResult<IEnumerable<BolumDto>>>
    {
        public class GetBolumDtoQueryHandler : IRequestHandler<GetBolumDtoQuery, IDataResult<IEnumerable<BolumDto>>>
        {
            private readonly IBolumRepository _bolumRepository;
            private readonly IMediator _mediator;
           

            public GetBolumDtoQueryHandler(IBolumRepository bolumRepository, IMediator mediator)
            {
                _bolumRepository = bolumRepository;
                _mediator = mediator;
             
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<BolumDto>>> Handle(GetBolumDtoQuery request, CancellationToken cancellationToken)
            {
                var result =   _bolumRepository.GetBolumDtoAsync();
                return new SuccessDataResult<IEnumerable<BolumDto>>(result, "BolumDto getirildi!");
            }
        }
    }
}
