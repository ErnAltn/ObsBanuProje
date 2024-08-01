
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
using Business.Handlers.ST_DersTurus.ValidationRules;

namespace Business.Handlers.ST_DersTurus.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateST_DersTuruCommand : IRequest<IResult>
    {

        public string Ad { get; set; }
        public string Ekstra { get; set; }


        public class CreateST_DersTuruCommandHandler : IRequestHandler<CreateST_DersTuruCommand, IResult>
        {
            private readonly IST_DersTuruRepository _sT_DersTuruRepository;
            private readonly IMediator _mediator;
            public CreateST_DersTuruCommandHandler(IST_DersTuruRepository sT_DersTuruRepository, IMediator mediator)
            {
                _sT_DersTuruRepository = sT_DersTuruRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateST_DersTuruValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateST_DersTuruCommand request, CancellationToken cancellationToken)
            {
                var isThereST_DersTuruRecord = _sT_DersTuruRepository.Query().Any(u => u.Ad == request.Ad);

                if (isThereST_DersTuruRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedST_DersTuru = new ST_DersTuru
                {
                    Ad = request.Ad,
                    Ekstra = request.Ekstra,

                };

                _sT_DersTuruRepository.Add(addedST_DersTuru);
                await _sT_DersTuruRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}