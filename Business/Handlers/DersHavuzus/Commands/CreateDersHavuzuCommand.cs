
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
using Business.Handlers.DersHavuzus.ValidationRules;
using System;

namespace Business.Handlers.DersHavuzus.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateDersHavuzuCommand : IRequest<IResult>
    {
        public int Id { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public System.DateTime DeletedDate { get; set; }
        public int DersDiliId { get; set; }
        public int DersSeviyesiId { get; set; }
        public int DersturuId { get; set; }
        public string DersKodu { get; set; }
        public string DersAdi { get; set; }
        public int Teorik { get; set; }
        public int Uygulama { get; set; }
        public float Kredi { get; set; }
        public int ECTS { get; set; }


        public class CreateDersHavuzuCommandHandler : IRequestHandler<CreateDersHavuzuCommand, IResult>
        {
            private readonly IDersHavuzuRepository _dersHavuzuRepository;
            private readonly IMediator _mediator;
            public CreateDersHavuzuCommandHandler(IDersHavuzuRepository dersHavuzuRepository, IMediator mediator)
            {
                _dersHavuzuRepository = dersHavuzuRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateDersHavuzuValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateDersHavuzuCommand request, CancellationToken cancellationToken)
            {
                var isThereDersHavuzuRecord = _dersHavuzuRepository.Query().Any(u => u.Id == request.Id);

                if (isThereDersHavuzuRecord == true)
                    return new ErrorResult(Messages.NameAlreadyExist);

                var addedDersHavuzu = new DersHavuzu
                {
                    CreatedDate = DateTime.Now,
                    DersDiliId = request.DersDiliId,
                    DersSeviyesiId = request.DersSeviyesiId,
                    DersturuId = request.DersturuId,
                    DersKodu = request.DersKodu,
                    DersAdi = request.DersAdi,
                    Teorik = request.Teorik,
                    Uygulama = request.Uygulama,
                    Kredi = request.Kredi,
                    ECTS = request.ECTS,

                };

                _dersHavuzuRepository.Add(addedDersHavuzu);
                await _dersHavuzuRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}