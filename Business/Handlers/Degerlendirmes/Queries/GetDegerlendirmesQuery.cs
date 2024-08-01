
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;

namespace Business.Handlers.Degerlendirmes.Queries
{

    public class GetDegerlendirmesQuery : IRequest<IDataResult<IEnumerable<Degerlendirme>>>
    {
        public class GetDegerlendirmesQueryHandler : IRequestHandler<GetDegerlendirmesQuery, IDataResult<IEnumerable<Degerlendirme>>>
        {
            private readonly IDegerlendirmeRepository _degerlendirmeRepository;
            private readonly IMediator _mediator;

            public GetDegerlendirmesQueryHandler(IDegerlendirmeRepository degerlendirmeRepository, IMediator mediator)
            {
                _degerlendirmeRepository = degerlendirmeRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Degerlendirme>>> Handle(GetDegerlendirmesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Degerlendirme>>(await _degerlendirmeRepository.GetListAsync());
            }
        }
    }
}