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

namespace Business.Handlers.Ogrencis.Queries
{
    public class GetOgrenciNotBilgisiQuery : IRequest<IDataResult<IEnumerable<OgrenciSinavDto>>>
    {
        public int Id { get; set; }

        public class GetOgrenciNotBilgisiQueryHandler : IRequestHandler<GetOgrenciNotBilgisiQuery, IDataResult<IEnumerable<OgrenciSinavDto>>>
        {
            private readonly IOgrenciRepository _ogrenciRepository;
            private readonly IMediator _mediator;

            public GetOgrenciNotBilgisiQueryHandler(IOgrenciRepository ogrenciRepository, IMediator mediator)
            {
                _ogrenciRepository = ogrenciRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]


            public async Task<IDataResult<IEnumerable<OgrenciSinavDto>>> Handle(GetOgrenciNotBilgisiQuery request, CancellationToken cancellationToken)
            {
                var result = _ogrenciRepository.GetOgrenciNotBilgisiAsync(request.Id);
                return new SuccessDataResult<IEnumerable<OgrenciSinavDto>>(result);
            }
        }
    }
}
