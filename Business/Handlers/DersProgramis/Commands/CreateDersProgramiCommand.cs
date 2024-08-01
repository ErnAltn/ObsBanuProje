
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
using Business.Handlers.DersProgramis.ValidationRules;
using System;

namespace Business.Handlers.DersProgramis.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateDersProgramiCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public System.DateTime DeletedDate { get; set; }
        public int DersAcmaId { get; set; }
        public int DerslikId { get; set; }
        public int DersGunuId { get; set; }
        public string DersSaati { get; set; }


        public class CreateDersProgramiCommandHandler : IRequestHandler<CreateDersProgramiCommand, IResult>
        {
            private readonly IDersProgramiRepository _dersProgramiRepository;
            private readonly IMediator _mediator;
            public CreateDersProgramiCommandHandler(IDersProgramiRepository dersProgramiRepository, IMediator mediator)
            {
                _dersProgramiRepository = dersProgramiRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateDersProgramiValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateDersProgramiCommand request, CancellationToken cancellationToken)
            {
                var isThereDersProgramiRecord = _dersProgramiRepository.Query().Any(u => u.Id == request.Id);

                if (isThereDersProgramiRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedDersProgrami = new DersProgrami
                {
                    CreatedDate = DateTime.Now,
                    DersAcmaId = request.DersAcmaId,
                    DerslikId = request.DerslikId,
                    DersGunuId = request.DersGunuId,
                    DersSaati = request.DersSaati,

                };

                _dersProgramiRepository.Add(addedDersProgrami);
                await _dersProgramiRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}