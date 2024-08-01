
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
using Business.Handlers.DersHavuzus.ValidationRules;
using System;

namespace Business.Handlers.DersHavuzus.Commands
{


    public class UpdateDersHavuzuCommand : IRequest<IResult>
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

        public class UpdateDersHavuzuCommandHandler : IRequestHandler<UpdateDersHavuzuCommand, IResult>
        {
            private readonly IDersHavuzuRepository _dersHavuzuRepository;
            private readonly IMediator _mediator;

            public UpdateDersHavuzuCommandHandler(IDersHavuzuRepository dersHavuzuRepository, IMediator mediator)
            {
                _dersHavuzuRepository = dersHavuzuRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateDersHavuzuValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateDersHavuzuCommand request, CancellationToken cancellationToken)
            {
                var isThereDersHavuzuRecord = await _dersHavuzuRepository.GetAsync(u => u.Id == request.Id);



                isThereDersHavuzuRecord.UpdatedDate = DateTime.Now;
                isThereDersHavuzuRecord.DersDiliId = request.DersDiliId;
                isThereDersHavuzuRecord.DersSeviyesiId = request.DersSeviyesiId;
                isThereDersHavuzuRecord.DersturuId = request.DersturuId;
                isThereDersHavuzuRecord.DersKodu = request.DersKodu;
                isThereDersHavuzuRecord.DersAdi = request.DersAdi;
                isThereDersHavuzuRecord.Teorik = request.Teorik;
                isThereDersHavuzuRecord.Uygulama = request.Uygulama;
                isThereDersHavuzuRecord.Kredi = request.Kredi;
                isThereDersHavuzuRecord.ECTS = request.ECTS;


                _dersHavuzuRepository.Update(isThereDersHavuzuRecord);
                await _dersHavuzuRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

