using Amazon.Runtime.Internal;
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

namespace Business.Handlers.OgretimElemanis.Queries
{
    public class GetOgretimElemaniOzlukBilgileriQuery:IRequest<IDataResult<IEnumerable<OzlukBilgileriDto>>>
    {
        public int userId { get; set; }

        public class GetOgretimElemaniOzlukBilgileriQueryHandler : IRequestHandler<GetOgretimElemaniOzlukBilgileriQuery, IDataResult<IEnumerable<OzlukBilgileriDto>>>
        {
            private readonly IOgretimElemaniRepository _ogretimElemaniRepository;
            private readonly IMediator _mediator;

            public GetOgretimElemaniOzlukBilgileriQueryHandler(IOgretimElemaniRepository ogretimElemaniRepository, IMediator mediator)
            {
                _ogretimElemaniRepository = ogretimElemaniRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<OzlukBilgileriDto>>> Handle(GetOgretimElemaniOzlukBilgileriQuery request, CancellationToken cancellationToken)
            {
                var result = await _ogretimElemaniRepository.GetOgretimElemaniOzlukBilgileri(request.userId);
                return new SuccessDataResult<IEnumerable<OzlukBilgileriDto>>(result, "Ozluk bilgileri getirildi.");
            }
        }
    }
}
