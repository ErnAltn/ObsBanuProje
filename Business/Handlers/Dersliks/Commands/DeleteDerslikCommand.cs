
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

namespace Business.Handlers.Dersliks.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteDerslikCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteDerslikCommandHandler : IRequestHandler<DeleteDerslikCommand, IResult>
        {
            private readonly IDerslikRepository _derslikRepository;
            private readonly IMediator _mediator;

            public DeleteDerslikCommandHandler(IDerslikRepository derslikRepository, IMediator mediator)
            {
                _derslikRepository = derslikRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteDerslikCommand request, CancellationToken cancellationToken)
            {
                var derslikToDelete = _derslikRepository.Get(p => p.Id == request.Id);

                derslikToDelete.DeletedDate = DateTime.Now;

                _derslikRepository.Update(derslikToDelete);
                await _derslikRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

