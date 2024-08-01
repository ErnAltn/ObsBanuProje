
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
using Business.Handlers.DersAcmas.ValidationRules;
using System;

namespace Business.Handlers.DersAcmas.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateDersAcmaCommand : IRequest<IResult>
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


        public class CreateDersAcmaCommandHandler : IRequestHandler<CreateDersAcmaCommand, IResult>
        {
            private readonly IDersAcmaRepository _dersAcmaRepository;
            private readonly IMediator _mediator;
            public CreateDersAcmaCommandHandler(IDersAcmaRepository dersAcmaRepository, IMediator mediator)
            {
                _dersAcmaRepository = dersAcmaRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateDersAcmaValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateDersAcmaCommand request, CancellationToken cancellationToken)
            {
                var isThereDersAcmaRecord = _dersAcmaRepository.Query().Any(u => u.Id == request.Id);

                if (isThereDersAcmaRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedDersAcma = new DersAcma
                {
                    CreatedDate = DateTime.Now,
                    AkademikYilId = request.AkademikYilId,
                    AkademikDonemId = request.AkademikDonemId,
                    MufredatId = request.MufredatId,
                    OgrElmId = request.OgrElmId,
                    Kontenjan = request.Kontenjan,

                };

                _dersAcmaRepository.Add(addedDersAcma);
                await _dersAcmaRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}