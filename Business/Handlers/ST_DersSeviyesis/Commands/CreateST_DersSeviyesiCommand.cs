
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
using Business.Handlers.ST_DersSeviyesis.ValidationRules;

namespace Business.Handlers.ST_DersSeviyesis.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateST_DersSeviyesiCommand : IRequest<IResult>
    {

        public string Ad { get; set; }
        public string Ekstra { get; set; }


        public class CreateST_DersSeviyesiCommandHandler : IRequestHandler<CreateST_DersSeviyesiCommand, IResult>
        {
            private readonly IST_DersSeviyesiRepository _sT_DersSeviyesiRepository;
            private readonly IMediator _mediator;
            public CreateST_DersSeviyesiCommandHandler(IST_DersSeviyesiRepository sT_DersSeviyesiRepository, IMediator mediator)
            {
                _sT_DersSeviyesiRepository = sT_DersSeviyesiRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateST_DersSeviyesiValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateST_DersSeviyesiCommand request, CancellationToken cancellationToken)
            {
                var isThereST_DersSeviyesiRecord = _sT_DersSeviyesiRepository.Query().Any(u => u.Ad == request.Ad);

                if (isThereST_DersSeviyesiRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedST_DersSeviyesi = new ST_DersSeviyesi
                {
                    Ad = request.Ad,
                    Ekstra = request.Ekstra,

                };

                _sT_DersSeviyesiRepository.Add(addedST_DersSeviyesi);
                await _sT_DersSeviyesiRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}