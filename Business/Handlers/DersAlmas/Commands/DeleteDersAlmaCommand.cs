
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

namespace Business.Handlers.DersAlmas.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteDersAlmaCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteDersAlmaCommandHandler : IRequestHandler<DeleteDersAlmaCommand, IResult>
        {
            private readonly IDersAlmaRepository _dersAlmaRepository;
            private readonly IMediator _mediator;

            public DeleteDersAlmaCommandHandler(IDersAlmaRepository dersAlmaRepository, IMediator mediator)
            {
                _dersAlmaRepository = dersAlmaRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteDersAlmaCommand request, CancellationToken cancellationToken)
            {
                var dersAlmaToDelete = _dersAlmaRepository.Get(p => p.Id == request.Id);

                dersAlmaToDelete.DeletedDate=DateTime.Now;

                _dersAlmaRepository.Update(dersAlmaToDelete);
                await _dersAlmaRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

