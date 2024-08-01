
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

namespace Business.Handlers.ST_AkademikDonems.Queries
{

    public class GetST_AkademikDonemsQuery : IRequest<IDataResult<IEnumerable<ST_AkademikDonem>>>
    {
        public class GetST_AkademikDonemsQueryHandler : IRequestHandler<GetST_AkademikDonemsQuery, IDataResult<IEnumerable<ST_AkademikDonem>>>
        {
            private readonly IST_AkademikDonemRepository _sT_AkademikDonemRepository;
            private readonly IMediator _mediator;

            public GetST_AkademikDonemsQueryHandler(IST_AkademikDonemRepository sT_AkademikDonemRepository, IMediator mediator)
            {
                _sT_AkademikDonemRepository = sT_AkademikDonemRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<ST_AkademikDonem>>> Handle(GetST_AkademikDonemsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<ST_AkademikDonem>>(await _sT_AkademikDonemRepository.GetListAsync());
            }
        }
    }
}