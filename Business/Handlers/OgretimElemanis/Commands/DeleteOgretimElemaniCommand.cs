
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

namespace Business.Handlers.OgretimElemanis.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteOgretimElemaniCommand : IRequest<IResult>
    {
        public int Id { get; set; }

        public class DeleteOgretimElemaniCommandHandler : IRequestHandler<DeleteOgretimElemaniCommand, IResult>
        {
            private readonly IOgretimElemaniRepository _ogretimElemaniRepository;
            private readonly IMediator _mediator;

            public DeleteOgretimElemaniCommandHandler(IOgretimElemaniRepository ogretimElemaniRepository, IMediator mediator)
            {
                _ogretimElemaniRepository = ogretimElemaniRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteOgretimElemaniCommand request, CancellationToken cancellationToken)
            {
                var ogretimElemaniToDelete = _ogretimElemaniRepository.Get(p => p.Id == request.Id);

                ogretimElemaniToDelete.DeletedDate = DateTime.Now;

                _ogretimElemaniRepository.Update(ogretimElemaniToDelete);
                await _ogretimElemaniRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

