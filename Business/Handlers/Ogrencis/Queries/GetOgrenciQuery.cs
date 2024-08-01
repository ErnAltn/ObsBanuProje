
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Ogrencis.Queries
{
    public class GetOgrenciQuery : IRequest<IDataResult<Ogrenci>>
    {
        public int Id { get; set; }

        public class GetOgrenciQueryHandler : IRequestHandler<GetOgrenciQuery, IDataResult<Ogrenci>>
        {
            private readonly IOgrenciRepository _ogrenciRepository;
            private readonly IMediator _mediator;

            public GetOgrenciQueryHandler(IOgrenciRepository ogrenciRepository, IMediator mediator)
            {
                _ogrenciRepository = ogrenciRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Ogrenci>> Handle(GetOgrenciQuery request, CancellationToken cancellationToken)
            {
                var ogrenci = await _ogrenciRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<Ogrenci>(ogrenci);
            }
        }
    }
}
