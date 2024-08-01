
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
using Business.Handlers.ST_AkademikYils.ValidationRules;

namespace Business.Handlers.ST_AkademikYils.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateST_AkademikYilCommand : IRequest<IResult>
    {

        public string Ad { get; set; }
        public string Ekstra { get; set; }


        public class CreateST_AkademikYilCommandHandler : IRequestHandler<CreateST_AkademikYilCommand, IResult>
        {
            private readonly IST_AkademikYilRepository _sT_AkademikYilRepository;
            private readonly IMediator _mediator;
            public CreateST_AkademikYilCommandHandler(IST_AkademikYilRepository sT_AkademikYilRepository, IMediator mediator)
            {
                _sT_AkademikYilRepository = sT_AkademikYilRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateST_AkademikYilValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateST_AkademikYilCommand request, CancellationToken cancellationToken)
            {
                var isThereST_AkademikYilRecord = _sT_AkademikYilRepository.Query().Any(u => u.Ad == request.Ad);

                if (isThereST_AkademikYilRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedST_AkademikYil = new ST_AkademikYil
                {
                    Ad = request.Ad,
                    Ekstra = request.Ekstra,

                };

                _sT_AkademikYilRepository.Add(addedST_AkademikYil);
                await _sT_AkademikYilRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}