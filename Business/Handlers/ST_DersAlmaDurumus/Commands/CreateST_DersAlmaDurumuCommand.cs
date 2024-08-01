
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
using Business.Handlers.ST_DersAlmaDurumus.ValidationRules;

namespace Business.Handlers.ST_DersAlmaDurumus.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateST_DersAlmaDurumuCommand : IRequest<IResult>
    {

        public string Ad { get; set; }
        public string Ekstra { get; set; }


        public class CreateST_DersAlmaDurumuCommandHandler : IRequestHandler<CreateST_DersAlmaDurumuCommand, IResult>
        {
            private readonly IST_DersAlmaDurumuRepository _sT_DersAlmaDurumuRepository;
            private readonly IMediator _mediator;
            public CreateST_DersAlmaDurumuCommandHandler(IST_DersAlmaDurumuRepository sT_DersAlmaDurumuRepository, IMediator mediator)
            {
                _sT_DersAlmaDurumuRepository = sT_DersAlmaDurumuRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateST_DersAlmaDurumuValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateST_DersAlmaDurumuCommand request, CancellationToken cancellationToken)
            {
                var isThereST_DersAlmaDurumuRecord = _sT_DersAlmaDurumuRepository.Query().Any(u => u.Ad == request.Ad);

                if (isThereST_DersAlmaDurumuRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedST_DersAlmaDurumu = new ST_DersAlmaDurumu
                {
                    Ad = request.Ad,
                    Ekstra = request.Ekstra,

                };

                _sT_DersAlmaDurumuRepository.Add(addedST_DersAlmaDurumu);
                await _sT_DersAlmaDurumuRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}