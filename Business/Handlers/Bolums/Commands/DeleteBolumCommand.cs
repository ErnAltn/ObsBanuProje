
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
using System;

namespace Business.Handlers.Bolums.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteBolumCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteBolumCommandHandler : IRequestHandler<DeleteBolumCommand, IResult>
        {
            private readonly IBolumRepository _bolumRepository;
            private readonly IMediator _mediator;

            public DeleteBolumCommandHandler(IBolumRepository bolumRepository, IMediator mediator)
            {
                _bolumRepository = bolumRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteBolumCommand request, CancellationToken cancellationToken)
            {
                var bolumToDelete = _bolumRepository.Get(p => p.Id == request.Id);
                    
                bolumToDelete.DeletedDate=DateTime.Now;

                _bolumRepository.Update(bolumToDelete);
                await _bolumRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

