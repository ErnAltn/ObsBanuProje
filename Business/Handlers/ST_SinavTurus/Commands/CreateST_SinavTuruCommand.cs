
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
using Business.Handlers.ST_SinavTurus.ValidationRules;

namespace Business.Handlers.ST_SinavTurus.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateST_SinavTuruCommand : IRequest<IResult>
    {

        public string Ad { get; set; }
        public string Ekstra { get; set; }


        public class CreateST_SinavTuruCommandHandler : IRequestHandler<CreateST_SinavTuruCommand, IResult>
        {
            private readonly IST_SinavTuruRepository _sT_SinavTuruRepository;
            private readonly IMediator _mediator;
            public CreateST_SinavTuruCommandHandler(IST_SinavTuruRepository sT_SinavTuruRepository, IMediator mediator)
            {
                _sT_SinavTuruRepository = sT_SinavTuruRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateST_SinavTuruValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateST_SinavTuruCommand request, CancellationToken cancellationToken)
            {
                var isThereST_SinavTuruRecord = _sT_SinavTuruRepository.Query().Any(u => u.Ad == request.Ad);

                if (isThereST_SinavTuruRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedST_SinavTuru = new ST_SinavTuru
                {
                    Ad = request.Ad,
                    Ekstra = request.Ekstra,

                };

                _sT_SinavTuruRepository.Add(addedST_SinavTuru);
                await _sT_SinavTuruRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}