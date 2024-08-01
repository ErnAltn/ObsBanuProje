
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
using Business.Handlers.Mufredats.ValidationRules;
using System;

namespace Business.Handlers.Mufredats.Commands
{


    public class UpdateMufredatCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public System.DateTime DeletedDate { get; set; }
        public int BolumId { get; set; }
        public int DersId { get; set; }
        public int AkedemikYilId { get; set; }
        public int AkedemikDonemId { get; set; }
        public int DersDonemi { get; set; }

        public class UpdateMufredatCommandHandler : IRequestHandler<UpdateMufredatCommand, IResult>
        {
            private readonly IMufredatRepository _mufredatRepository;
            private readonly IMediator _mediator;

            public UpdateMufredatCommandHandler(IMufredatRepository mufredatRepository, IMediator mediator)
            {
                _mufredatRepository = mufredatRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateMufredatValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateMufredatCommand request, CancellationToken cancellationToken)
            {
                var isThereMufredatRecord = await _mufredatRepository.GetAsync(u => u.Id == request.Id);


                isThereMufredatRecord.UpdatedDate = DateTime.Now;
                isThereMufredatRecord.BolumId = request.BolumId;
                isThereMufredatRecord.DersId = request.DersId;
                isThereMufredatRecord.AkedemikYilId = request.AkedemikYilId;
                isThereMufredatRecord.AkedemikDonemId = request.AkedemikDonemId;
                isThereMufredatRecord.DersDonemi = request.DersDonemi;


                _mufredatRepository.Update(isThereMufredatRecord);
                await _mufredatRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

