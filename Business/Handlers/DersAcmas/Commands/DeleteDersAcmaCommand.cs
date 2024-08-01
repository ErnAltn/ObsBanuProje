
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

namespace Business.Handlers.DersAcmas.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteDersAcmaCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteDersAcmaCommandHandler : IRequestHandler<DeleteDersAcmaCommand, IResult>
        {
            private readonly IDersAcmaRepository _dersAcmaRepository;
            private readonly IMediator _mediator;

            public DeleteDersAcmaCommandHandler(IDersAcmaRepository dersAcmaRepository, IMediator mediator)
            {
                _dersAcmaRepository = dersAcmaRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteDersAcmaCommand request, CancellationToken cancellationToken)
            {
                var dersAcmaToDelete = _dersAcmaRepository.Get(p => p.Id == request.Id);

                dersAcmaToDelete.DeletedDate=DateTime.Now;

                _dersAcmaRepository.Update(dersAcmaToDelete);
                await _dersAcmaRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

