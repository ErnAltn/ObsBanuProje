
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
using Business.Handlers.ST_DersGunus.ValidationRules;


namespace Business.Handlers.ST_DersGunus.Commands
{


    public class UpdateST_DersGunuCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Ekstra { get; set; }

        public class UpdateST_DersGunuCommandHandler : IRequestHandler<UpdateST_DersGunuCommand, IResult>
        {
            private readonly IST_DersGunuRepository _sT_DersGunuRepository;
            private readonly IMediator _mediator;

            public UpdateST_DersGunuCommandHandler(IST_DersGunuRepository sT_DersGunuRepository, IMediator mediator)
            {
                _sT_DersGunuRepository = sT_DersGunuRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateST_DersGunuValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateST_DersGunuCommand request, CancellationToken cancellationToken)
            {
                var isThereST_DersGunuRecord = await _sT_DersGunuRepository.GetAsync(u => u.Id == request.Id);


                isThereST_DersGunuRecord.Ad = request.Ad;
                isThereST_DersGunuRecord.Ekstra = request.Ekstra;


                _sT_DersGunuRepository.Update(isThereST_DersGunuRecord);
                await _sT_DersGunuRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

