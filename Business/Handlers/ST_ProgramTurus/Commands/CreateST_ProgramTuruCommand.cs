
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
using Business.Handlers.ST_ProgramTurus.ValidationRules;

namespace Business.Handlers.ST_ProgramTurus.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateST_ProgramTuruCommand : IRequest<IResult>
    {

        public string Ad { get; set; }
        public string Ekstra { get; set; }


        public class CreateST_ProgramTuruCommandHandler : IRequestHandler<CreateST_ProgramTuruCommand, IResult>
        {
            private readonly IST_ProgramTuruRepository _sT_ProgramTuruRepository;
            private readonly IMediator _mediator;
            public CreateST_ProgramTuruCommandHandler(IST_ProgramTuruRepository sT_ProgramTuruRepository, IMediator mediator)
            {
                _sT_ProgramTuruRepository = sT_ProgramTuruRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateST_ProgramTuruValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateST_ProgramTuruCommand request, CancellationToken cancellationToken)
            {
                var isThereST_ProgramTuruRecord = _sT_ProgramTuruRepository.Query().Any(u => u.Ad == request.Ad);

                if (isThereST_ProgramTuruRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedST_ProgramTuru = new ST_ProgramTuru
                {
                    Ad = request.Ad,
                    Ekstra = request.Ekstra,

                };

                _sT_ProgramTuruRepository.Add(addedST_ProgramTuru);
                await _sT_ProgramTuruRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}