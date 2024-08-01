
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
using Business.Handlers.ST_OgretimDilis.ValidationRules;

namespace Business.Handlers.ST_OgretimDilis.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateST_OgretimDiliCommand : IRequest<IResult>
    {

        public string Ad { get; set; }
        public string Ekstra { get; set; }


        public class CreateST_OgretimDiliCommandHandler : IRequestHandler<CreateST_OgretimDiliCommand, IResult>
        {
            private readonly IST_OgretimDiliRepository _sT_OgretimDiliRepository;
            private readonly IMediator _mediator;
            public CreateST_OgretimDiliCommandHandler(IST_OgretimDiliRepository sT_OgretimDiliRepository, IMediator mediator)
            {
                _sT_OgretimDiliRepository = sT_OgretimDiliRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateST_OgretimDiliValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateST_OgretimDiliCommand request, CancellationToken cancellationToken)
            {
                var isThereST_OgretimDiliRecord = _sT_OgretimDiliRepository.Query().Any(u => u.Ad == request.Ad);

                if (isThereST_OgretimDiliRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedST_OgretimDili = new ST_OgretimDili
                {
                    Ad = request.Ad,
                    Ekstra = request.Ekstra,

                };

                _sT_OgretimDiliRepository.Add(addedST_OgretimDili);
                await _sT_OgretimDiliRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}