using Business.BusinessAspects;
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
    public class GetOgretimElemaniSinavlarQuery:IRequest<IDataResult<IEnumerable<OgretimElemaniSınavlarDto>>>
    {
        public int Id { get; set; }
        public class GetOgretimElemaniSinavlarQueryHandler : IRequestHandler<GetOgretimElemaniSinavlarQuery, IDataResult<IEnumerable<OgretimElemaniSınavlarDto>>>
        {
            private readonly IOgretimElemaniRepository _ogretimElemaniRepository;
            private readonly IMediator _mediator;

            public GetOgretimElemaniSinavlarQueryHandler(IOgretimElemaniRepository ogretimElemaniRepository, IMediator mediator)
            {
                _ogretimElemaniRepository = ogretimElemaniRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
          

            public async Task<IDataResult<IEnumerable<OgretimElemaniSınavlarDto>>> Handle(GetOgretimElemaniSinavlarQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<OgretimElemaniSınavlarDto>>(_ogretimElemaniRepository.GetOgretimElemaniSinavlar(request.Id));
            }
        }
    }
}
