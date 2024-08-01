
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
using Business.Handlers.Degerlendirmes.ValidationRules;
using System;

namespace Business.Handlers.Degerlendirmes.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateDegerlendirmeCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public System.DateTime DeletedDate { get; set; }
        public int SinavId { get; set; }
        public int OgrenciId { get; set; }
        public float SinavNotu { get; set; }


        public class CreateDegerlendirmeCommandHandler : IRequestHandler<CreateDegerlendirmeCommand, IResult>
        {
            private readonly IDegerlendirmeRepository _degerlendirmeRepository;
            private readonly IMediator _mediator;
            public CreateDegerlendirmeCommandHandler(IDegerlendirmeRepository degerlendirmeRepository, IMediator mediator)
            {
                _degerlendirmeRepository = degerlendirmeRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateDegerlendirmeValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateDegerlendirmeCommand request, CancellationToken cancellationToken)
            {
                var isThereDegerlendirmeRecord = _degerlendirmeRepository.Query().Any(u => u.Id == request.Id);

                if (isThereDegerlendirmeRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedDegerlendirme = new Degerlendirme
                {
                    CreatedDate = DateTime.Now,
                    SinavId = request.SinavId,
                    OgrenciId = request.OgrenciId,
                    SinavNotu = request.SinavNotu,

                };

                _degerlendirmeRepository.Add(addedDegerlendirme);
                await _degerlendirmeRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}