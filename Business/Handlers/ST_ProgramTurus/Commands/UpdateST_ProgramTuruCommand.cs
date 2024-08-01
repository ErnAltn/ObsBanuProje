
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
using Business.Handlers.ST_ProgramTurus.ValidationRules;


namespace Business.Handlers.ST_ProgramTurus.Commands
{


    public class UpdateST_ProgramTuruCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Ekstra { get; set; }

        public class UpdateST_ProgramTuruCommandHandler : IRequestHandler<UpdateST_ProgramTuruCommand, IResult>
        {
            private readonly IST_ProgramTuruRepository _sT_ProgramTuruRepository;
            private readonly IMediator _mediator;

            public UpdateST_ProgramTuruCommandHandler(IST_ProgramTuruRepository sT_ProgramTuruRepository, IMediator mediator)
            {
                _sT_ProgramTuruRepository = sT_ProgramTuruRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateST_ProgramTuruValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateST_ProgramTuruCommand request, CancellationToken cancellationToken)
            {
                var isThereST_ProgramTuruRecord = await _sT_ProgramTuruRepository.GetAsync(u => u.Id == request.Id);


                isThereST_ProgramTuruRecord.Ad = request.Ad;
                isThereST_ProgramTuruRecord.Ekstra = request.Ekstra;


                _sT_ProgramTuruRepository.Update(isThereST_ProgramTuruRecord);
                await _sT_ProgramTuruRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

