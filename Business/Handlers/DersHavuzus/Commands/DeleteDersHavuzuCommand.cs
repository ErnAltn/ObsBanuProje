
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

namespace Business.Handlers.DersHavuzus.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteDersHavuzuCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteDersHavuzuCommandHandler : IRequestHandler<DeleteDersHavuzuCommand, IResult>
        {
            private readonly IDersHavuzuRepository _dersHavuzuRepository;
            private readonly IMediator _mediator;

            public DeleteDersHavuzuCommandHandler(IDersHavuzuRepository dersHavuzuRepository, IMediator mediator)
            {
                _dersHavuzuRepository = dersHavuzuRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteDersHavuzuCommand request, CancellationToken cancellationToken)
            {
                var dersHavuzuToDelete = _dersHavuzuRepository.Get(p => p.Id == request.Id);

                dersHavuzuToDelete.DeletedDate = DateTime.Now;

                _dersHavuzuRepository.Update(dersHavuzuToDelete);
                await _dersHavuzuRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

