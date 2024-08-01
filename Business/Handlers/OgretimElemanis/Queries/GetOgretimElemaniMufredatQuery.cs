using Business.BusinessAspects;
using Business.Handlers.Mufredats.Queries;
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

namespace Business.Handlers.OgretimElemanis.Queries
{
    public class GetOgretimElemaniMufredatQuery:IRequest<IDataResult<IEnumerable<MufredatDto>>>
    {
        public int Id { get; set; }
        public class GetOgretimElemaniMufredatQueryHandler : IRequestHandler<GetOgretimElemaniMufredatQuery, IDataResult<IEnumerable<MufredatDto>>>
        {
            private readonly IOgretimElemaniRepository _ogretimElemaniRepository;
            private readonly IMediator _mediator;

            public GetOgretimElemaniMufredatQueryHandler(IOgretimElemaniRepository ogretimElemaniRepository, IMediator mediator)
            {
                _ogretimElemaniRepository = ogretimElemaniRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<MufredatDto>>> Handle(GetOgretimElemaniMufredatQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<MufredatDto>>(_ogretimElemaniRepository.GetOgretimElemaniMufredat(request.Id));
            }
        }
    }
}
