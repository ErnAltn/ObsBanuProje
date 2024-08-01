
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
using Business.Handlers.ST_DersDilis.ValidationRules;

namespace Business.Handlers.ST_DersDilis.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateST_DersDiliCommand : IRequest<IResult>
    {

        public string Ad { get; set; }
        public string Ekstra { get; set; }


        public class CreateST_DersDiliCommandHandler : IRequestHandler<CreateST_DersDiliCommand, IResult>
        {
            private readonly IST_DersDiliRepository _sT_DersDiliRepository;
            private readonly IMediator _mediator;
            public CreateST_DersDiliCommandHandler(IST_DersDiliRepository sT_DersDiliRepository, IMediator mediator)
            {
                _sT_DersDiliRepository = sT_DersDiliRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateST_DersDiliValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateST_DersDiliCommand request, CancellationToken cancellationToken)
            {
                var isThereST_DersDiliRecord = _sT_DersDiliRepository.Query().Any(u => u.Ad == request.Ad);

                if (isThereST_DersDiliRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedST_DersDili = new ST_DersDili
                {
                    Ad = request.Ad,
                    Ekstra = request.Ekstra,

                };

                _sT_DersDiliRepository.Add(addedST_DersDili);
                await _sT_DersDiliRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}