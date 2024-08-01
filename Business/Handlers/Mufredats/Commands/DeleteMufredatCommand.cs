
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

namespace Business.Handlers.Mufredats.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteMufredatCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteMufredatCommandHandler : IRequestHandler<DeleteMufredatCommand, IResult>
        {
            private readonly IMufredatRepository _mufredatRepository;
            private readonly IMediator _mediator;

            public DeleteMufredatCommandHandler(IMufredatRepository mufredatRepository, IMediator mediator)
            {
                _mufredatRepository = mufredatRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteMufredatCommand request, CancellationToken cancellationToken)
            {
                var mufredatToDelete = _mufredatRepository.Get(p => p.Id == request.Id);

                mufredatToDelete.DeletedDate=DateTime.Now;

                _mufredatRepository.Update(mufredatToDelete);
                await _mufredatRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

