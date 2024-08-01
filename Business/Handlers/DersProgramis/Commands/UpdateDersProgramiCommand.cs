
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
using Business.Handlers.DersProgramis.ValidationRules;
using System;

namespace Business.Handlers.DersProgramis.Commands
{


    public class UpdateDersProgramiCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public System.DateTime DeletedDate { get; set; }
        public int DersAcmaId { get; set; }
        public int DerslikId { get; set; }
        public int DersGunuId { get; set; }
        public string DersSaati { get; set; }

        public class UpdateDersProgramiCommandHandler : IRequestHandler<UpdateDersProgramiCommand, IResult>
        {
            private readonly IDersProgramiRepository _dersProgramiRepository;
            private readonly IMediator _mediator;

            public UpdateDersProgramiCommandHandler(IDersProgramiRepository dersProgramiRepository, IMediator mediator)
            {
                _dersProgramiRepository = dersProgramiRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateDersProgramiValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateDersProgramiCommand request, CancellationToken cancellationToken)
            {
                var isThereDersProgramiRecord = await _dersProgramiRepository.GetAsync(u => u.Id == request.Id);


                isThereDersProgramiRecord.UpdatedDate = DateTime.Now;
                isThereDersProgramiRecord.DersAcmaId = request.DersAcmaId;
                isThereDersProgramiRecord.DerslikId = request.DerslikId;
                isThereDersProgramiRecord.DersGunuId = request.DersGunuId;
                isThereDersProgramiRecord.DersSaati = request.DersSaati;


                _dersProgramiRepository.Update(isThereDersProgramiRecord);
                await _dersProgramiRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

