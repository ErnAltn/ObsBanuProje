
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Dersliks.Queries
{
    public class GetDerslikQuery : IRequest<IDataResult<Derslik>>
    {
        public int Id { get; set; }

        public class GetDerslikQueryHandler : IRequestHandler<GetDerslikQuery, IDataResult<Derslik>>
        {
            private readonly IDerslikRepository _derslikRepository;
            private readonly IMediator _mediator;

            public GetDerslikQueryHandler(IDerslikRepository derslikRepository, IMediator mediator)
            {
                _derslikRepository = derslikRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Derslik>> Handle(GetDerslikQuery request, CancellationToken cancellationToken)
            {
                var derslik = await _derslikRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<Derslik>(derslik);
            }
        }
    }
}
