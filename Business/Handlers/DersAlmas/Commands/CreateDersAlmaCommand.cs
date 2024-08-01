
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
using Business.Handlers.DersAlmas.ValidationRules;
using System;

namespace Business.Handlers.DersAlmas.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateDersAlmaCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public System.DateTime DeletedDate { get; set; }
        public int DersAcmaId { get; set; }
        public int OgrenciId { get; set; }
        public int DersDurumId { get; set; }


        public class CreateDersAlmaCommandHandler : IRequestHandler<CreateDersAlmaCommand, IResult>
        {
            private readonly IDersAlmaRepository _dersAlmaRepository;
            private readonly IMediator _mediator;
            public CreateDersAlmaCommandHandler(IDersAlmaRepository dersAlmaRepository, IMediator mediator)
            {
                _dersAlmaRepository = dersAlmaRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateDersAlmaValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateDersAlmaCommand request, CancellationToken cancellationToken)
            {
                var isThereDersAlmaRecord = _dersAlmaRepository.Query().Any(u => u.Id == request.Id);

                if (isThereDersAlmaRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedDersAlma = new DersAlma
                {
                    CreatedDate = DateTime.Now,
                    DersAcmaId = request.DersAcmaId,
                    OgrenciId = request.OgrenciId,
                    DersDurumId = request.DersDurumId,

                };

                _dersAlmaRepository.Add(addedDersAlma);
                await _dersAlmaRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}