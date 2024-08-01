
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.DersHavuzus.Queries
{
    public class GetDersHavuzuQuery : IRequest<IDataResult<DersHavuzu>>
    {
        public int Id { get; set; }

        public class GetDersHavuzuQueryHandler : IRequestHandler<GetDersHavuzuQuery, IDataResult<DersHavuzu>>
        {
            private readonly IDersHavuzuRepository _dersHavuzuRepository;
            private readonly IMediator _mediator;

            public GetDersHavuzuQueryHandler(IDersHavuzuRepository dersHavuzuRepository, IMediator mediator)
            {
                _dersHavuzuRepository = dersHavuzuRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<DersHavuzu>> Handle(GetDersHavuzuQuery request, CancellationToken cancellationToken)
            {
                var dersHavuzu = await _dersHavuzuRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<DersHavuzu>(dersHavuzu);
            }
        }
    }
}
