
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.ST_AkademikDonems.Queries
{
    public class GetST_AkademikDonemQuery : IRequest<IDataResult<ST_AkademikDonem>>
    {
        public int Id { get; set; }

        public class GetST_AkademikDonemQueryHandler : IRequestHandler<GetST_AkademikDonemQuery, IDataResult<ST_AkademikDonem>>
        {
            private readonly IST_AkademikDonemRepository _sT_AkademikDonemRepository;
            private readonly IMediator _mediator;

            public GetST_AkademikDonemQueryHandler(IST_AkademikDonemRepository sT_AkademikDonemRepository, IMediator mediator)
            {
                _sT_AkademikDonemRepository = sT_AkademikDonemRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<ST_AkademikDonem>> Handle(GetST_AkademikDonemQuery request, CancellationToken cancellationToken)
            {
                var sT_AkademikDonem = await _sT_AkademikDonemRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<ST_AkademikDonem>(sT_AkademikDonem);
            }
        }
    }
}
