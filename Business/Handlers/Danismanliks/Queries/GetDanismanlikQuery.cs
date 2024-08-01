
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Danismanliks.Queries
{
    public class GetDanismanlikQuery : IRequest<IDataResult<Danismanlik>>
    {
        public int Id { get; set; }

        public class GetDanismanlikQueryHandler : IRequestHandler<GetDanismanlikQuery, IDataResult<Danismanlik>>
        {
            private readonly IDanismanlikRepository _danismanlikRepository;
            private readonly IMediator _mediator;

            public GetDanismanlikQueryHandler(IDanismanlikRepository danismanlikRepository, IMediator mediator)
            {
                _danismanlikRepository = danismanlikRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Danismanlik>> Handle(GetDanismanlikQuery request, CancellationToken cancellationToken)
            {
                var danismanlik = await _danismanlikRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<Danismanlik>(danismanlik);
            }
        }
    }
}
