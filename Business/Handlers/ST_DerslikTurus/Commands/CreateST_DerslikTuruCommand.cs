
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
using Business.Handlers.ST_DerslikTurus.ValidationRules;

namespace Business.Handlers.ST_DerslikTurus.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateST_DerslikTuruCommand : IRequest<IResult>
    {

        public string Ad { get; set; }
        public string Ekstra { get; set; }


        public class CreateST_DerslikTuruCommandHandler : IRequestHandler<CreateST_DerslikTuruCommand, IResult>
        {
            private readonly IST_DerslikTuruRepository _sT_DerslikTuruRepository;
            private readonly IMediator _mediator;
            public CreateST_DerslikTuruCommandHandler(IST_DerslikTuruRepository sT_DerslikTuruRepository, IMediator mediator)
            {
                _sT_DerslikTuruRepository = sT_DerslikTuruRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateST_DerslikTuruValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateST_DerslikTuruCommand request, CancellationToken cancellationToken)
            {
                var isThereST_DerslikTuruRecord = _sT_DerslikTuruRepository.Query().Any(u => u.Ad == request.Ad);

                if (isThereST_DerslikTuruRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedST_DerslikTuru = new ST_DerslikTuru
                {
                    Ad = request.Ad,
                    Ekstra = request.Ekstra,

                };

                _sT_DerslikTuruRepository.Add(addedST_DerslikTuru);
                await _sT_DerslikTuruRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}