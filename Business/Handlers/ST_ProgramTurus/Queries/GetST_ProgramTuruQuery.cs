
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.ST_ProgramTurus.Queries
{
    public class GetST_ProgramTuruQuery : IRequest<IDataResult<ST_ProgramTuru>>
    {
        public int Id { get; set; }

        public class GetST_ProgramTuruQueryHandler : IRequestHandler<GetST_ProgramTuruQuery, IDataResult<ST_ProgramTuru>>
        {
            private readonly IST_ProgramTuruRepository _sT_ProgramTuruRepository;
            private readonly IMediator _mediator;

            public GetST_ProgramTuruQueryHandler(IST_ProgramTuruRepository sT_ProgramTuruRepository, IMediator mediator)
            {
                _sT_ProgramTuruRepository = sT_ProgramTuruRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<ST_ProgramTuru>> Handle(GetST_ProgramTuruQuery request, CancellationToken cancellationToken)
            {
                var sT_ProgramTuru = await _sT_ProgramTuruRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<ST_ProgramTuru>(sT_ProgramTuru);
            }
        }
    }
}
