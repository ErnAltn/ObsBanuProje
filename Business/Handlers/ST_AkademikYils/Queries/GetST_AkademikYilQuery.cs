
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.ST_AkademikYils.Queries
{
    public class GetST_AkademikYilQuery : IRequest<IDataResult<ST_AkademikYil>>
    {
        public int Id { get; set; }

        public class GetST_AkademikYilQueryHandler : IRequestHandler<GetST_AkademikYilQuery, IDataResult<ST_AkademikYil>>
        {
            private readonly IST_AkademikYilRepository _sT_AkademikYilRepository;
            private readonly IMediator _mediator;

            public GetST_AkademikYilQueryHandler(IST_AkademikYilRepository sT_AkademikYilRepository, IMediator mediator)
            {
                _sT_AkademikYilRepository = sT_AkademikYilRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<ST_AkademikYil>> Handle(GetST_AkademikYilQuery request, CancellationToken cancellationToken)
            {
                var sT_AkademikYil = await _sT_AkademikYilRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<ST_AkademikYil>(sT_AkademikYil);
            }
        }
    }
}
