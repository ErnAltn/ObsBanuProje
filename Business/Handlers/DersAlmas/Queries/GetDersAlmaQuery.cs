
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.DersAlmas.Queries
{
    public class GetDersAlmaQuery : IRequest<IDataResult<DersAlma>>
    {
        public int Id { get; set; }

        public class GetDersAlmaQueryHandler : IRequestHandler<GetDersAlmaQuery, IDataResult<DersAlma>>
        {
            private readonly IDersAlmaRepository _dersAlmaRepository;
            private readonly IMediator _mediator;

            public GetDersAlmaQueryHandler(IDersAlmaRepository dersAlmaRepository, IMediator mediator)
            {
                _dersAlmaRepository = dersAlmaRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<DersAlma>> Handle(GetDersAlmaQuery request, CancellationToken cancellationToken)
            {
                var dersAlma = await _dersAlmaRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<DersAlma>(dersAlma);
            }
        }
    }
}
