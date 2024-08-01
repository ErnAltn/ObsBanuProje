
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
using Business.Handlers.ST_DerslikTurus.ValidationRules;


namespace Business.Handlers.ST_DerslikTurus.Commands
{


    public class UpdateST_DerslikTuruCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Ekstra { get; set; }

        public class UpdateST_DerslikTuruCommandHandler : IRequestHandler<UpdateST_DerslikTuruCommand, IResult>
        {
            private readonly IST_DerslikTuruRepository _sT_DerslikTuruRepository;
            private readonly IMediator _mediator;

            public UpdateST_DerslikTuruCommandHandler(IST_DerslikTuruRepository sT_DerslikTuruRepository, IMediator mediator)
            {
                _sT_DerslikTuruRepository = sT_DerslikTuruRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateST_DerslikTuruValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateST_DerslikTuruCommand request, CancellationToken cancellationToken)
            {
                var isThereST_DerslikTuruRecord = await _sT_DerslikTuruRepository.GetAsync(u => u.Id == request.Id);


                isThereST_DerslikTuruRecord.Ad = request.Ad;
                isThereST_DerslikTuruRecord.Ekstra = request.Ekstra;


                _sT_DerslikTuruRepository.Update(isThereST_DerslikTuruRecord);
                await _sT_DerslikTuruRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

