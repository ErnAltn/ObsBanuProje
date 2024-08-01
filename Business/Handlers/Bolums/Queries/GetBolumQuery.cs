
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Bolums.Queries
{
    public class GetBolumQuery : IRequest<IDataResult<Bolum>>
    {
        public int Id { get; set; }

        public class GetBolumQueryHandler : IRequestHandler<GetBolumQuery, IDataResult<Bolum>>
        {
            private readonly IBolumRepository _bolumRepository;
            private readonly IMediator _mediator;

            public GetBolumQueryHandler(IBolumRepository bolumRepository, IMediator mediator)
            {
                _bolumRepository = bolumRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Bolum>> Handle(GetBolumQuery request, CancellationToken cancellationToken)
            {
                var bolum = await _bolumRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<Bolum>(bolum);
            }
        }
    }
}
