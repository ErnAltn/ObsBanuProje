
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
using Business.Handlers.ST_AkademikDonems.ValidationRules;

namespace Business.Handlers.ST_AkademikDonems.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateST_AkademikDonemCommand : IRequest<IResult>
    {

        public string Ad { get; set; }
        public string Ekstra { get; set; }


        public class CreateST_AkademikDonemCommandHandler : IRequestHandler<CreateST_AkademikDonemCommand, IResult>
        {
            private readonly IST_AkademikDonemRepository _sT_AkademikDonemRepository;
            private readonly IMediator _mediator;
            public CreateST_AkademikDonemCommandHandler(IST_AkademikDonemRepository sT_AkademikDonemRepository, IMediator mediator)
            {
                _sT_AkademikDonemRepository = sT_AkademikDonemRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateST_AkademikDonemValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateST_AkademikDonemCommand request, CancellationToken cancellationToken)
            {
                var isThereST_AkademikDonemRecord = _sT_AkademikDonemRepository.Query().Any(u => u.Ad == request.Ad);

                if (isThereST_AkademikDonemRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedST_AkademikDonem = new ST_AkademikDonem
                {
                    Ad = request.Ad,
                    Ekstra = request.Ekstra,

                };

                _sT_AkademikDonemRepository.Add(addedST_AkademikDonem);
                await _sT_AkademikDonemRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}