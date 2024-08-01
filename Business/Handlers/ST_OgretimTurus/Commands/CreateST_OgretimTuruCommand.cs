
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
using Business.Handlers.ST_OgretimTurus.ValidationRules;

namespace Business.Handlers.ST_OgretimTurus.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateST_OgretimTuruCommand : IRequest<IResult>
    {

        public string Ad { get; set; }
        public string Ekstra { get; set; }


        public class CreateST_OgretimTuruCommandHandler : IRequestHandler<CreateST_OgretimTuruCommand, IResult>
        {
            private readonly IST_OgretimTuruRepository _sT_OgretimTuruRepository;
            private readonly IMediator _mediator;
            public CreateST_OgretimTuruCommandHandler(IST_OgretimTuruRepository sT_OgretimTuruRepository, IMediator mediator)
            {
                _sT_OgretimTuruRepository = sT_OgretimTuruRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateST_OgretimTuruValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateST_OgretimTuruCommand request, CancellationToken cancellationToken)
            {
                var isThereST_OgretimTuruRecord = _sT_OgretimTuruRepository.Query().Any(u => u.Ad == request.Ad);

                if (isThereST_OgretimTuruRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedST_OgretimTuru = new ST_OgretimTuru
                {
                    Ad = request.Ad,
                    Ekstra = request.Ekstra,

                };

                _sT_OgretimTuruRepository.Add(addedST_OgretimTuru);
                await _sT_OgretimTuruRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}