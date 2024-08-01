
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
using Business.Handlers.ST_AkademikYils.ValidationRules;


namespace Business.Handlers.ST_AkademikYils.Commands
{


    public class UpdateST_AkademikYilCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Ekstra { get; set; }

        public class UpdateST_AkademikYilCommandHandler : IRequestHandler<UpdateST_AkademikYilCommand, IResult>
        {
            private readonly IST_AkademikYilRepository _sT_AkademikYilRepository;
            private readonly IMediator _mediator;

            public UpdateST_AkademikYilCommandHandler(IST_AkademikYilRepository sT_AkademikYilRepository, IMediator mediator)
            {
                _sT_AkademikYilRepository = sT_AkademikYilRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateST_AkademikYilValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateST_AkademikYilCommand request, CancellationToken cancellationToken)
            {
                var isThereST_AkademikYilRecord = await _sT_AkademikYilRepository.GetAsync(u => u.Id == request.Id);


                isThereST_AkademikYilRecord.Ad = request.Ad;
                isThereST_AkademikYilRecord.Ekstra = request.Ekstra;


                _sT_AkademikYilRepository.Update(isThereST_AkademikYilRecord);
                await _sT_AkademikYilRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

