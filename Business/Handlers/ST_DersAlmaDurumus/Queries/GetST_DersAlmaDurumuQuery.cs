
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.ST_DersAlmaDurumus.Queries
{
    public class GetST_DersAlmaDurumuQuery : IRequest<IDataResult<ST_DersAlmaDurumu>>
    {
        public int Id { get; set; }

        public class GetST_DersAlmaDurumuQueryHandler : IRequestHandler<GetST_DersAlmaDurumuQuery, IDataResult<ST_DersAlmaDurumu>>
        {
            private readonly IST_DersAlmaDurumuRepository _sT_DersAlmaDurumuRepository;
            private readonly IMediator _mediator;

            public GetST_DersAlmaDurumuQueryHandler(IST_DersAlmaDurumuRepository sT_DersAlmaDurumuRepository, IMediator mediator)
            {
                _sT_DersAlmaDurumuRepository = sT_DersAlmaDurumuRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<ST_DersAlmaDurumu>> Handle(GetST_DersAlmaDurumuQuery request, CancellationToken cancellationToken)
            {
                var sT_DersAlmaDurumu = await _sT_DersAlmaDurumuRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<ST_DersAlmaDurumu>(sT_DersAlmaDurumu);
            }
        }
    }
}
