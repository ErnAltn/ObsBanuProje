
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.ST_DersSeviyesis.Queries
{
    public class GetST_DersSeviyesiQuery : IRequest<IDataResult<ST_DersSeviyesi>>
    {
        public int Id { get; set; }

        public class GetST_DersSeviyesiQueryHandler : IRequestHandler<GetST_DersSeviyesiQuery, IDataResult<ST_DersSeviyesi>>
        {
            private readonly IST_DersSeviyesiRepository _sT_DersSeviyesiRepository;
            private readonly IMediator _mediator;

            public GetST_DersSeviyesiQueryHandler(IST_DersSeviyesiRepository sT_DersSeviyesiRepository, IMediator mediator)
            {
                _sT_DersSeviyesiRepository = sT_DersSeviyesiRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<ST_DersSeviyesi>> Handle(GetST_DersSeviyesiQuery request, CancellationToken cancellationToken)
            {
                var sT_DersSeviyesi = await _sT_DersSeviyesiRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<ST_DersSeviyesi>(sT_DersSeviyesi);
            }
        }
    }
}
