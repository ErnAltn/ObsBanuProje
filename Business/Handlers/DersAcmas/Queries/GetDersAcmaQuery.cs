
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.DersAcmas.Queries
{
    public class GetDersAcmaQuery : IRequest<IDataResult<DersAcma>>
    {
        public int Id { get; set; }

        public class GetDersAcmaQueryHandler : IRequestHandler<GetDersAcmaQuery, IDataResult<DersAcma>>
        {
            private readonly IDersAcmaRepository _dersAcmaRepository;
            private readonly IMediator _mediator;

            public GetDersAcmaQueryHandler(IDersAcmaRepository dersAcmaRepository, IMediator mediator)
            {
                _dersAcmaRepository = dersAcmaRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<DersAcma>> Handle(GetDersAcmaQuery request, CancellationToken cancellationToken)
            {
                var dersAcma = await _dersAcmaRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<DersAcma>(dersAcma);
            }
        }
    }
}
