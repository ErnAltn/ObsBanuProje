
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

namespace Business.Handlers.Danismanliks.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteDanismanlikCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteDanismanlikCommandHandler : IRequestHandler<DeleteDanismanlikCommand, IResult>
        {
            private readonly IDanismanlikRepository _danismanlikRepository;
            private readonly IMediator _mediator;

            public DeleteDanismanlikCommandHandler(IDanismanlikRepository danismanlikRepository, IMediator mediator)
            {
                _danismanlikRepository = danismanlikRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteDanismanlikCommand request, CancellationToken cancellationToken)
            {
                var danismanlikToDelete = _danismanlikRepository.Get(p => p.Id == request.Id);

                danismanlikToDelete.DeletedDate=DateTime.Now;

                _danismanlikRepository.Update(danismanlikToDelete);
                await _danismanlikRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

