
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
using Business.Handlers.ST_OgretimTurus.ValidationRules;


namespace Business.Handlers.ST_OgretimTurus.Commands
{


    public class UpdateST_OgretimTuruCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Ekstra { get; set; }

        public class UpdateST_OgretimTuruCommandHandler : IRequestHandler<UpdateST_OgretimTuruCommand, IResult>
        {
            private readonly IST_OgretimTuruRepository _sT_OgretimTuruRepository;
            private readonly IMediator _mediator;

            public UpdateST_OgretimTuruCommandHandler(IST_OgretimTuruRepository sT_OgretimTuruRepository, IMediator mediator)
            {
                _sT_OgretimTuruRepository = sT_OgretimTuruRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateST_OgretimTuruValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateST_OgretimTuruCommand request, CancellationToken cancellationToken)
            {
                var isThereST_OgretimTuruRecord = await _sT_OgretimTuruRepository.GetAsync(u => u.Id == request.Id);


                isThereST_OgretimTuruRecord.Ad = request.Ad;
                isThereST_OgretimTuruRecord.Ekstra = request.Ekstra;


                _sT_OgretimTuruRepository.Update(isThereST_OgretimTuruRecord);
                await _sT_OgretimTuruRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

