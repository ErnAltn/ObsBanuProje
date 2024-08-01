
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.DersProgramis.Queries
{
    public class GetDersProgramiQuery : IRequest<IDataResult<DersProgrami>>
    {
        public int Id { get; set; }

        public class GetDersProgramiQueryHandler : IRequestHandler<GetDersProgramiQuery, IDataResult<DersProgrami>>
        {
            private readonly IDersProgramiRepository _dersProgramiRepository;
            private readonly IMediator _mediator;

            public GetDersProgramiQueryHandler(IDersProgramiRepository dersProgramiRepository, IMediator mediator)
            {
                _dersProgramiRepository = dersProgramiRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<DersProgrami>> Handle(GetDersProgramiQuery request, CancellationToken cancellationToken)
            {
                var dersProgrami = await _dersProgramiRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<DersProgrami>(dersProgrami);
            }
        }
    }
}
