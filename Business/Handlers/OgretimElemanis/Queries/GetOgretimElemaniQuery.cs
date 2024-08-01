
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.OgretimElemanis.Queries
{
    public class GetOgretimElemaniQuery : IRequest<IDataResult<OgretimElemani>>
    {
        public int Id { get; set; }

        public class GetOgretimElemaniQueryHandler : IRequestHandler<GetOgretimElemaniQuery, IDataResult<OgretimElemani>>
        {
            private readonly IOgretimElemaniRepository _ogretimElemaniRepository;
            private readonly IMediator _mediator;

            public GetOgretimElemaniQueryHandler(IOgretimElemaniRepository ogretimElemaniRepository, IMediator mediator)
            {
                _ogretimElemaniRepository = ogretimElemaniRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<OgretimElemani>> Handle(GetOgretimElemaniQuery request, CancellationToken cancellationToken)
            {
                var ogretimElemani = await _ogretimElemaniRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<OgretimElemani>(ogretimElemani);
            }
        }
    }
}
