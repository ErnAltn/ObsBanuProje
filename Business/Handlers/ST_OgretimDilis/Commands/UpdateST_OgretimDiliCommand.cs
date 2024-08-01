
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
using Business.Handlers.ST_OgretimDilis.ValidationRules;


namespace Business.Handlers.ST_OgretimDilis.Commands
{


    public class UpdateST_OgretimDiliCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Ekstra { get; set; }

        public class UpdateST_OgretimDiliCommandHandler : IRequestHandler<UpdateST_OgretimDiliCommand, IResult>
        {
            private readonly IST_OgretimDiliRepository _sT_OgretimDiliRepository;
            private readonly IMediator _mediator;

            public UpdateST_OgretimDiliCommandHandler(IST_OgretimDiliRepository sT_OgretimDiliRepository, IMediator mediator)
            {
                _sT_OgretimDiliRepository = sT_OgretimDiliRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateST_OgretimDiliValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateST_OgretimDiliCommand request, CancellationToken cancellationToken)
            {
                var isThereST_OgretimDiliRecord = await _sT_OgretimDiliRepository.GetAsync(u => u.Id == request.Id);


                isThereST_OgretimDiliRecord.Ad = request.Ad;
                isThereST_OgretimDiliRecord.Ekstra = request.Ekstra;


                _sT_OgretimDiliRepository.Update(isThereST_OgretimDiliRecord);
                await _sT_OgretimDiliRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

