
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.ST_ProgramTurus.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteST_ProgramTuruCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteST_ProgramTuruCommandHandler : IRequestHandler<DeleteST_ProgramTuruCommand, IResult>
        {
            private readonly IST_ProgramTuruRepository _sT_ProgramTuruRepository;
            private readonly IMediator _mediator;

            public DeleteST_ProgramTuruCommandHandler(IST_ProgramTuruRepository sT_ProgramTuruRepository, IMediator mediator)
            {
                _sT_ProgramTuruRepository = sT_ProgramTuruRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteST_ProgramTuruCommand request, CancellationToken cancellationToken)
            {
                var sT_ProgramTuruToDelete = _sT_ProgramTuruRepository.Get(p => p.Id == request.Id);

                _sT_ProgramTuruRepository.Delete(sT_ProgramTuruToDelete);
                await _sT_ProgramTuruRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

