using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.Ogrencis.Queries
{
    public class GetOgrenciDtoQuery:IRequest<IDataResult<IEnumerable<OgrenciDto>>>
    {
        public class GetOgrenciDtoQueryHandler : IRequestHandler<GetOgrenciDtoQuery, IDataResult<IEnumerable<OgrenciDto>>>
        {
            private readonly IOgrenciRepository _ogrenciRepository;
            private readonly IMediator _mediator;

            public GetOgrenciDtoQueryHandler(IOgrenciRepository ogrenciRepository, IMediator mediator)
            {
                _ogrenciRepository = ogrenciRepository;
                _mediator = mediator;
            }

            public async Task<IDataResult<IEnumerable<OgrenciDto>>> Handle(GetOgrenciDtoQuery request, CancellationToken cancellationToken)
            {
                var result = _ogrenciRepository.GetOgrenciDto();
                return new SuccessDataResult<IEnumerable<OgrenciDto>>(result);
            }
        }
    }
}
