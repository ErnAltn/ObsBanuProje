
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
using Business.Handlers.ST_DersTurus.ValidationRules;


namespace Business.Handlers.ST_DersTurus.Commands
{


    public class UpdateST_DersTuruCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Ekstra { get; set; }

        public class UpdateST_DersTuruCommandHandler : IRequestHandler<UpdateST_DersTuruCommand, IResult>
        {
            private readonly IST_DersTuruRepository _sT_DersTuruRepository;
            private readonly IMediator _mediator;

            public UpdateST_DersTuruCommandHandler(IST_DersTuruRepository sT_DersTuruRepository, IMediator mediator)
            {
                _sT_DersTuruRepository = sT_DersTuruRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateST_DersTuruValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateST_DersTuruCommand request, CancellationToken cancellationToken)
            {
                var isThereST_DersTuruRecord = await _sT_DersTuruRepository.GetAsync(u => u.Id == request.Id);


                isThereST_DersTuruRecord.Ad = request.Ad;
                isThereST_DersTuruRecord.Ekstra = request.Ekstra;


                _sT_DersTuruRepository.Update(isThereST_DersTuruRecord);
                await _sT_DersTuruRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

