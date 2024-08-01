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
    public class GetOgretimElemaniVerilenDerslerQuery:IRequest<IDataResult<IEnumerable<OgretimElemaniVerilenDerslerDto>>>
    {
        public int Id { get; set; }
        public class GetOgretimElemaniVerilenDerslerQueryHandler : IRequestHandler<GetOgretimElemaniVerilenDerslerQuery, IDataResult<IEnumerable<OgretimElemaniVerilenDerslerDto>>>
        {
            private readonly IOgretimElemaniRepository _ogretimElemaniRepository;
            private readonly IMediator _mediator;

            public GetOgretimElemaniVerilenDerslerQueryHandler(IOgretimElemaniRepository ogretimElemaniRepository, IMediator mediator)
            {
                _ogretimElemaniRepository = ogretimElemaniRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<OgretimElemaniVerilenDerslerDto>>> Handle(GetOgretimElemaniVerilenDerslerQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<OgretimElemaniVerilenDerslerDto>>(_ogretimElemaniRepository.GetOgretimElemaniVerilenDersler(request.Id));
            }
        }
    }
}
