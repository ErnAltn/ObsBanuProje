
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
using Business.Handlers.Mufredats.ValidationRules;
using System;

namespace Business.Handlers.Mufredats.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateMufredatCommand : IRequest<IResult>
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


        public class CreateMufredatCommandHandler : IRequestHandler<CreateMufredatCommand, IResult>
        {
            private readonly IMufredatRepository _mufredatRepository;
            private readonly IMediator _mediator;
            public CreateMufredatCommandHandler(IMufredatRepository mufredatRepository, IMediator mediator)
            {
                _mufredatRepository = mufredatRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateMufredatValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateMufredatCommand request, CancellationToken cancellationToken)
            {
                var isThereMufredatRecord = _mufredatRepository.Query().Any(u => u.Id == request.Id);

                if (isThereMufredatRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedMufredat = new Mufredat
                {
                    CreatedDate = DateTime.Now,
                    BolumId = request.BolumId,
                    DersId = request.DersId,
                    AkedemikYilId = request.AkedemikYilId,
                    AkedemikDonemId = request.AkedemikDonemId,
                    DersDonemi = request.DersDonemi,

                };

                _mufredatRepository.Add(addedMufredat);
                await _mufredatRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}