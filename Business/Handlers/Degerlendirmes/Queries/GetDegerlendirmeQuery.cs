
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Degerlendirmes.Queries
{
    public class GetDegerlendirmeQuery : IRequest<IDataResult<Degerlendirme>>
    {
        public int Id { get; set; }

        public class GetDegerlendirmeQueryHandler : IRequestHandler<GetDegerlendirmeQuery, IDataResult<Degerlendirme>>
        {
            private readonly IDegerlendirmeRepository _degerlendirmeRepository;
            private readonly IMediator _mediator;

            public GetDegerlendirmeQueryHandler(IDegerlendirmeRepository degerlendirmeRepository, IMediator mediator)
            {
                _degerlendirmeRepository = degerlendirmeRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Degerlendirme>> Handle(GetDegerlendirmeQuery request, CancellationToken cancellationToken)
            {
                var degerlendirme = await _degerlendirmeRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<Degerlendirme>(degerlendirme);
            }
        }
    }
}
