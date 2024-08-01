
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
using Business.Handlers.ST_DersGunus.ValidationRules;

namespace Business.Handlers.ST_DersGunus.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateST_DersGunuCommand : IRequest<IResult>
    {

        public string Ad { get; set; }
        public string Ekstra { get; set; }


        public class CreateST_DersGunuCommandHandler : IRequestHandler<CreateST_DersGunuCommand, IResult>
        {
            private readonly IST_DersGunuRepository _sT_DersGunuRepository;
            private readonly IMediator _mediator;
            public CreateST_DersGunuCommandHandler(IST_DersGunuRepository sT_DersGunuRepository, IMediator mediator)
            {
                _sT_DersGunuRepository = sT_DersGunuRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateST_DersGunuValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateST_DersGunuCommand request, CancellationToken cancellationToken)
            {
                var isThereST_DersGunuRecord = _sT_DersGunuRepository.Query().Any(u => u.Ad == request.Ad);

                if (isThereST_DersGunuRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedST_DersGunu = new ST_DersGunu
                {
                    Ad = request.Ad,
                    Ekstra = request.Ekstra,

                };

                _sT_DersGunuRepository.Add(addedST_DersGunu);
                await _sT_DersGunuRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}