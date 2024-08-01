
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.ST_DersAlmaDurumus.ValidationRules;


namespace Business.Handlers.ST_DersAlmaDurumus.Commands
{


    public class UpdateST_DersAlmaDurumuCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Ekstra { get; set; }

        public class UpdateST_DersAlmaDurumuCommandHandler : IRequestHandler<UpdateST_DersAlmaDurumuCommand, IResult>
        {
            private readonly IST_DersAlmaDurumuRepository _sT_DersAlmaDurumuRepository;
            private readonly IMediator _mediator;

            public UpdateST_DersAlmaDurumuCommandHandler(IST_DersAlmaDurumuRepository sT_DersAlmaDurumuRepository, IMediator mediator)
            {
                _sT_DersAlmaDurumuRepository = sT_DersAlmaDurumuRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateST_DersAlmaDurumuValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateST_DersAlmaDurumuCommand request, CancellationToken cancellationToken)
            {
                var isThereST_DersAlmaDurumuRecord = await _sT_DersAlmaDurumuRepository.GetAsync(u => u.Id == request.Id);


                isThereST_DersAlmaDurumuRecord.Ad = request.Ad;
                isThereST_DersAlmaDurumuRecord.Ekstra = request.Ekstra;


                _sT_DersAlmaDurumuRepository.Update(isThereST_DersAlmaDurumuRecord);
                await _sT_DersAlmaDurumuRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

