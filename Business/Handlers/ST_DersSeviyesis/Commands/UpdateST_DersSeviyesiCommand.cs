
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
using Business.Handlers.ST_DersSeviyesis.ValidationRules;


namespace Business.Handlers.ST_DersSeviyesis.Commands
{


    public class UpdateST_DersSeviyesiCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Ekstra { get; set; }

        public class UpdateST_DersSeviyesiCommandHandler : IRequestHandler<UpdateST_DersSeviyesiCommand, IResult>
        {
            private readonly IST_DersSeviyesiRepository _sT_DersSeviyesiRepository;
            private readonly IMediator _mediator;

            public UpdateST_DersSeviyesiCommandHandler(IST_DersSeviyesiRepository sT_DersSeviyesiRepository, IMediator mediator)
            {
                _sT_DersSeviyesiRepository = sT_DersSeviyesiRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateST_DersSeviyesiValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateST_DersSeviyesiCommand request, CancellationToken cancellationToken)
            {
                var isThereST_DersSeviyesiRecord = await _sT_DersSeviyesiRepository.GetAsync(u => u.Id == request.Id);


                isThereST_DersSeviyesiRecord.Ad = request.Ad;
                isThereST_DersSeviyesiRecord.Ekstra = request.Ekstra;


                _sT_DersSeviyesiRepository.Update(isThereST_DersSeviyesiRecord);
                await _sT_DersSeviyesiRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

