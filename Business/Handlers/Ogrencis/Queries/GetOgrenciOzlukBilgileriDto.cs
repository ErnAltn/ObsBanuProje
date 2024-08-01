using Business.BusinessAspects;
using Business.Services.UserService.Abstract;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Ogrencis.Queries
{
    public class GetOgrenciOzlukBilgileriDto : IRequest<IDataResult<IEnumerable<OzlukBilgileriDto>>>
    {
        public int userId { get; set; }

        public class GetOgrenciOzlukBilgileriDtoHandler : IRequestHandler<GetOgrenciOzlukBilgileriDto, IDataResult<IEnumerable<OzlukBilgileriDto>>>
        {
            private readonly IOgrenciRepository _ogrenciRepository;
            private readonly IMediator _mediator;
            private readonly IUserService _userService;

            public GetOgrenciOzlukBilgileriDtoHandler(IOgrenciRepository ogrenciRepository, IMediator mediator, IUserService userService)
            {
                _ogrenciRepository = ogrenciRepository;
                _mediator = mediator;
                _userService = userService;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<OzlukBilgileriDto>>> Handle(GetOgrenciOzlukBilgileriDto request, CancellationToken cancellationToken)
            {
                var result = await _ogrenciRepository.GetOzlukBilgileriAsync(request.userId);
                return new SuccessDataResult<IEnumerable<OzlukBilgileriDto>>(result, "Ozluk bilgileri getirildi.");
            }
        }
    }
}
