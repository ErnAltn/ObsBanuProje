
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
using Business.Handlers.DersAlmas.ValidationRules;
using System;

namespace Business.Handlers.DersAlmas.Commands
{


    public class UpdateDersAlmaCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public System.DateTime DeletedDate { get; set; }
        public int DersAcmaId { get; set; }
        public int OgrenciId { get; set; }
        public int DersDurumId { get; set; }

        public class UpdateDersAlmaCommandHandler : IRequestHandler<UpdateDersAlmaCommand, IResult>
        {
            private readonly IDersAlmaRepository _dersAlmaRepository;
            private readonly IMediator _mediator;

            public UpdateDersAlmaCommandHandler(IDersAlmaRepository dersAlmaRepository, IMediator mediator)
            {
                _dersAlmaRepository = dersAlmaRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateDersAlmaValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateDersAlmaCommand request, CancellationToken cancellationToken)
            {
                var isThereDersAlmaRecord = await _dersAlmaRepository.GetAsync(u => u.Id == request.Id);



                isThereDersAlmaRecord.UpdatedDate = DateTime.Now;
                isThereDersAlmaRecord.DersAcmaId = request.DersAcmaId;
                isThereDersAlmaRecord.OgrenciId = request.OgrenciId;
                isThereDersAlmaRecord.DersDurumId = request.DersDurumId;


                _dersAlmaRepository.Update(isThereDersAlmaRecord);
                await _dersAlmaRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

