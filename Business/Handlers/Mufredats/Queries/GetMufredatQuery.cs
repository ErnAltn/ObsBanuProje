
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Mufredats.Queries
{
    public class GetMufredatQuery : IRequest<IDataResult<Mufredat>>
    {
        public int Id { get; set; }

        public class GetMufredatQueryHandler : IRequestHandler<GetMufredatQuery, IDataResult<Mufredat>>
        {
            private readonly IMufredatRepository _mufredatRepository;
            private readonly IMediator _mediator;

            public GetMufredatQueryHandler(IMufredatRepository mufredatRepository, IMediator mediator)
            {
                _mufredatRepository = mufredatRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Mufredat>> Handle(GetMufredatQuery request, CancellationToken cancellationToken)
            {
                var mufredat = await _mufredatRepository.GetAsync(p => p.Id == request.Id);
                return new SuccessDataResult<Mufredat>(mufredat);
            }
        }
    }
}
