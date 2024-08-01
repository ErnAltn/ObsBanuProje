
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
using Business.Handlers.Dersliks.ValidationRules;
using System;

namespace Business.Handlers.Dersliks.Commands
{


    public class UpdateDerslikCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public System.DateTime DeletedDate { get; set; }
        public int DerslikTuruId { get; set; }
        public string DerslikAdi { get; set; }
        public int Kapasite { get; set; }

        public class UpdateDerslikCommandHandler : IRequestHandler<UpdateDerslikCommand, IResult>
        {
            private readonly IDerslikRepository _derslikRepository;
            private readonly IMediator _mediator;

            public UpdateDerslikCommandHandler(IDerslikRepository derslikRepository, IMediator mediator)
            {
                _derslikRepository = derslikRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateDerslikValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateDerslikCommand request, CancellationToken cancellationToken)
            {
                var isThereDerslikRecord = await _derslikRepository.GetAsync(u => u.Id == request.Id);



                isThereDerslikRecord.UpdatedDate = DateTime.Now;
                isThereDerslikRecord.DerslikTuruId = request.DerslikTuruId;
                isThereDerslikRecord.DerslikAdi = request.DerslikAdi;
                isThereDerslikRecord.Kapasite = request.Kapasite;


                _derslikRepository.Update(isThereDerslikRecord);
                await _derslikRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

