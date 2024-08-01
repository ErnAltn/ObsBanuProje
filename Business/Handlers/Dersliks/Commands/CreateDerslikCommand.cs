
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
using Business.Handlers.Dersliks.ValidationRules;
using System;

namespace Business.Handlers.Dersliks.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateDerslikCommand : IRequest<IResult>
    {
        public int Id { get; set; } 
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public System.DateTime DeletedDate { get; set; }
        public int DerslikTuruId { get; set; }
        public string DerslikAdi { get; set; }
        public int Kapasite { get; set; }


        public class CreateDerslikCommandHandler : IRequestHandler<CreateDerslikCommand, IResult>
        {
            private readonly IDerslikRepository _derslikRepository;
            private readonly IMediator _mediator;
            public CreateDerslikCommandHandler(IDerslikRepository derslikRepository, IMediator mediator)
            {
                _derslikRepository = derslikRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateDerslikValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateDerslikCommand request, CancellationToken cancellationToken)
            {
                var isThereDerslikRecord = _derslikRepository.Query().Any(u => u.DerslikAdi == request.DerslikAdi);

                if (isThereDerslikRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedDerslik = new Derslik
                {
                    CreatedDate = DateTime.Now,
                    DerslikTuruId = request.DerslikTuruId,
                    DerslikAdi = request.DerslikAdi,
                    Kapasite = request.Kapasite,

                };

                _derslikRepository.Add(addedDerslik);
                await _derslikRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}