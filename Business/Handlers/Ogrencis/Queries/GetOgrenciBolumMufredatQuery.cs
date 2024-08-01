using Business.BusinessAspects;
using Business.Handlers.DersProgramis.Queries;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Ogrencis.Queries
{
    public class GetOgrenciBolumMufredatQuery: IRequest<IDataResult<IEnumerable<MufredatDto>>>
    {
        public int Id { get; set; }

        public class GetOgrenciBolumMufredatQueryHandler : IRequestHandler<GetOgrenciBolumMufredatQuery, IDataResult<IEnumerable<MufredatDto>>>
        {
            private readonly IMufredatRepository _mufredatRepository;
            private readonly IMediator _mediator;

            public GetOgrenciBolumMufredatQueryHandler(IMufredatRepository mufredatRepository, IMediator mediator)
            {
                _mufredatRepository = mufredatRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]

            public async Task<IDataResult<IEnumerable<MufredatDto>>> Handle(GetOgrenciBolumMufredatQuery request, CancellationToken cancellationToken)
            {
                var result = _mufredatRepository.GetOgrenciMufredat(request.Id);
                return new SuccessDataResult<IEnumerable<MufredatDto>>(result);
            }
        }
    }
}
