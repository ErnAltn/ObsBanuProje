using Business.BusinessAspects;
using Business.Handlers.DersProgramis.Queries;
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

namespace Business.Handlers.Mufredats.Queries
{
    public class GetMufredatDtoQuery:IRequest<IDataResult<IEnumerable<MufredatDto>>>
    {
        public class GetMufredatDtoQueryHandler : IRequestHandler<GetMufredatDtoQuery, IDataResult<IEnumerable<MufredatDto>>>
        {
            private readonly IMufredatRepository _mufredatRepository;
            private readonly IMediator _mediator;


            public GetMufredatDtoQueryHandler(IMufredatRepository mufredatRepository, IMediator mediator)
            {
                _mufredatRepository = mufredatRepository;
                _mediator = mediator;

            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<MufredatDto>>> Handle(GetMufredatDtoQuery request, CancellationToken cancellationToken)
            {
                var result = _mufredatRepository.GetMufredatDto();
                return new SuccessDataResult<IEnumerable<MufredatDto>>(result);
            }
        }
    }
}
