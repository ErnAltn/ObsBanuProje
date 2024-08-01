
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

namespace Business.Handlers.DersProgramis.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteDersProgramiCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteDersProgramiCommandHandler : IRequestHandler<DeleteDersProgramiCommand, IResult>
        {
            private readonly IDersProgramiRepository _dersProgramiRepository;
            private readonly IMediator _mediator;

            public DeleteDersProgramiCommandHandler(IDersProgramiRepository dersProgramiRepository, IMediator mediator)
            {
                _dersProgramiRepository = dersProgramiRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteDersProgramiCommand request, CancellationToken cancellationToken)
            {
                var dersProgramiToDelete = _dersProgramiRepository.Get(p => p.Id == request.Id);

                dersProgramiToDelete.DeletedDate=DateTime.Now;

                _dersProgramiRepository.Update(dersProgramiToDelete);
                await _dersProgramiRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

