
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.Danismanliks.ValidationRules;
using System;

namespace Business.Handlers.Danismanliks.Commands
{


    public class UpdateDanismanlikCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public System.DateTime DeletedDate { get; set; }
        public int OgrElmID { get; set; }
        public int OgrenciId { get; set; }

        public class UpdateDanismanlikCommandHandler : IRequestHandler<UpdateDanismanlikCommand, IResult>
        {
            private readonly IDanismanlikRepository _danismanlikRepository;
            private readonly IMediator _mediator;

            public UpdateDanismanlikCommandHandler(IDanismanlikRepository danismanlikRepository, IMediator mediator)
            {
                _danismanlikRepository = danismanlikRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateDanismanlikValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateDanismanlikCommand request, CancellationToken cancellationToken)
            {
                var isThereDanismanlikRecord = await _danismanlikRepository.GetAsync(u => u.Id == request.Id);


                isThereDanismanlikRecord.UpdatedDate =DateTime.Now;
                isThereDanismanlikRecord.OgrElmID = request.OgrElmID;
                isThereDanismanlikRecord.OgrenciId = request.OgrenciId;


                _danismanlikRepository.Update(isThereDanismanlikRecord);
                await _danismanlikRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

