
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
using Business.Handlers.DersAcmas.ValidationRules;
using System;

namespace Business.Handlers.DersAcmas.Commands
{


    public class UpdateDersAcmaCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public System.DateTime DeletedDate { get; set; }
        public int AkademikYilId { get; set; }
        public int AkademikDonemId { get; set; }
        public int MufredatId { get; set; }
        public int OgrElmId { get; set; }
        public int Kontenjan { get; set; }

        public class UpdateDersAcmaCommandHandler : IRequestHandler<UpdateDersAcmaCommand, IResult>
        {
            private readonly IDersAcmaRepository _dersAcmaRepository;
            private readonly IMediator _mediator;

            public UpdateDersAcmaCommandHandler(IDersAcmaRepository dersAcmaRepository, IMediator mediator)
            {
                _dersAcmaRepository = dersAcmaRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateDersAcmaValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateDersAcmaCommand request, CancellationToken cancellationToken)
            {
                var isThereDersAcmaRecord = await _dersAcmaRepository.GetAsync(u => u.Id == request.Id);



                isThereDersAcmaRecord.UpdatedDate = DateTime.Now;
                isThereDersAcmaRecord.AkademikYilId = request.AkademikYilId;
                isThereDersAcmaRecord.AkademikDonemId = request.AkademikDonemId;
                isThereDersAcmaRecord.MufredatId = request.MufredatId;
                isThereDersAcmaRecord.OgrElmId = request.OgrElmId;
                isThereDersAcmaRecord.Kontenjan = request.Kontenjan;


                _dersAcmaRepository.Update(isThereDersAcmaRecord);
                await _dersAcmaRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

