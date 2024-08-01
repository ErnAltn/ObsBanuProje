
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
using Business.Handlers.ST_DersDilis.ValidationRules;


namespace Business.Handlers.ST_DersDilis.Commands
{


    public class UpdateST_DersDiliCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Ekstra { get; set; }

        public class UpdateST_DersDiliCommandHandler : IRequestHandler<UpdateST_DersDiliCommand, IResult>
        {
            private readonly IST_DersDiliRepository _sT_DersDiliRepository;
            private readonly IMediator _mediator;

            public UpdateST_DersDiliCommandHandler(IST_DersDiliRepository sT_DersDiliRepository, IMediator mediator)
            {
                _sT_DersDiliRepository = sT_DersDiliRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateST_DersDiliValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateST_DersDiliCommand request, CancellationToken cancellationToken)
            {
                var isThereST_DersDiliRecord = await _sT_DersDiliRepository.GetAsync(u => u.Id == request.Id);


                isThereST_DersDiliRecord.Ad = request.Ad;
                isThereST_DersDiliRecord.Ekstra = request.Ekstra;


                _sT_DersDiliRepository.Update(isThereST_DersDiliRecord);
                await _sT_DersDiliRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

