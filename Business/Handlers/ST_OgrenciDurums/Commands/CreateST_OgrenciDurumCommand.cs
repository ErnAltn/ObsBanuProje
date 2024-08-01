
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.ST_OgrenciDurums.ValidationRules;

namespace Business.Handlers.ST_OgrenciDurums.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateST_OgrenciDurumCommand : IRequest<IResult>
    {

        public string Ad { get; set; }
        public string Ekstra { get; set; }


        public class CreateST_OgrenciDurumCommandHandler : IRequestHandler<CreateST_OgrenciDurumCommand, IResult>
        {
            private readonly IST_OgrenciDurumRepository _sT_OgrenciDurumRepository;
            private readonly IMediator _mediator;
            public CreateST_OgrenciDurumCommandHandler(IST_OgrenciDurumRepository sT_OgrenciDurumRepository, IMediator mediator)
            {
                _sT_OgrenciDurumRepository = sT_OgrenciDurumRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateST_OgrenciDurumValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateST_OgrenciDurumCommand request, CancellationToken cancellationToken)
            {
                var isThereST_OgrenciDurumRecord = _sT_OgrenciDurumRepository.Query().Any(u => u.Ad == request.Ad);

                if (isThereST_OgrenciDurumRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedST_OgrenciDurum = new ST_OgrenciDurum
                {
                    Ad = request.Ad,
                    Ekstra = request.Ekstra,

                };

                _sT_OgrenciDurumRepository.Add(addedST_OgrenciDurum);
                await _sT_OgrenciDurumRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}