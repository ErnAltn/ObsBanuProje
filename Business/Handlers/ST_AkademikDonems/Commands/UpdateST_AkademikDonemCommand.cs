
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
using Business.Handlers.ST_AkademikDonems.ValidationRules;


namespace Business.Handlers.ST_AkademikDonems.Commands
{


    public class UpdateST_AkademikDonemCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Ekstra { get; set; }

        public class UpdateST_AkademikDonemCommandHandler : IRequestHandler<UpdateST_AkademikDonemCommand, IResult>
        {
            private readonly IST_AkademikDonemRepository _sT_AkademikDonemRepository;
            private readonly IMediator _mediator;

            public UpdateST_AkademikDonemCommandHandler(IST_AkademikDonemRepository sT_AkademikDonemRepository, IMediator mediator)
            {
                _sT_AkademikDonemRepository = sT_AkademikDonemRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateST_AkademikDonemValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateST_AkademikDonemCommand request, CancellationToken cancellationToken)
            {
                var isThereST_AkademikDonemRecord = await _sT_AkademikDonemRepository.GetAsync(u => u.Id == request.Id);


                isThereST_AkademikDonemRecord.Ad = request.Ad;
                isThereST_AkademikDonemRecord.Ekstra = request.Ekstra;


                _sT_AkademikDonemRepository.Update(isThereST_AkademikDonemRecord);
                await _sT_AkademikDonemRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

