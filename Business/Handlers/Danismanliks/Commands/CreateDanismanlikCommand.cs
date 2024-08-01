
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.Danismanliks.ValidationRules;
using System;

namespace Business.Handlers.Danismanliks.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateDanismanlikCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public System.DateTime DeletedDate { get; set; }
        public int OgrElmID { get; set; }
        public int OgrenciId { get; set; }


        public class CreateDanismanlikCommandHandler : IRequestHandler<CreateDanismanlikCommand, IResult>
        {
            private readonly IDanismanlikRepository _danismanlikRepository;
            private readonly IMediator _mediator;
            public CreateDanismanlikCommandHandler(IDanismanlikRepository danismanlikRepository, IMediator mediator)
            {
                _danismanlikRepository = danismanlikRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateDanismanlikValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateDanismanlikCommand request, CancellationToken cancellationToken)
            {
                var isThereDanismanlikRecord = _danismanlikRepository.Query().Any(u => u.Id == request.Id);

                if (isThereDanismanlikRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedDanismanlik = new Danismanlik
                {
                    CreatedDate = DateTime.Now,
                    OgrElmID = request.OgrElmID,
                    OgrenciId = request.OgrenciId,

                };

                _danismanlikRepository.Add(addedDanismanlik);
                await _danismanlikRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}