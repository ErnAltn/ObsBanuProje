
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.ST_DersGunus.Queries
{
    public class GetST_DersGunuQuery : IRequest<IDataResult<ST_DersGunu>>
    {
        public int Id { get; set; }

        public class GetST_DersGunuQueryHandler : IRequestHandler<GetST_DersGunuQuery, IDataResult<ST_DersGunu>>
        {
            private readonly IST_DersGunuRepository _sT_DersGunuRepository;
            private readonly IMediator _mediator;

            public GetST_DersGunuQueryHandler(IST_DersGunuRepository sT_DersGunuRepository, IMediator mediator)
            {
                _sT_DersGunuRepository = sT_DersGunuRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<ST_DersGunu>> Handle(GetST_DersGunuQuery request, CancellationToken cancellationToken)
            {
                var sT_DersGunu = await _sT_DersGunuRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<ST_DersGunu>(sT_DersGunu);
            }
        }
    }
}
