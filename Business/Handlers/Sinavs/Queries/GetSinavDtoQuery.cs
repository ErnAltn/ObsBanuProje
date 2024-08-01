using Business.Handlers.Ogrencis.Queries;
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

namespace Business.Handlers.Sinavs.Queries
{
    public class GetSinavDtoQuery: IRequest<IDataResult<IEnumerable<SinavDto>>>
    {
        public class GetSinavDtoQueryHandler : IRequestHandler<GetSinavDtoQuery, IDataResult<IEnumerable<SinavDto>>>
        {
            private readonly ISinavRepository _sinavRepository;
            private readonly IMediator _mediator;

            public GetSinavDtoQueryHandler(ISinavRepository sinavRepository, IMediator mediator)
            {
                _sinavRepository = sinavRepository;
                _mediator = mediator;
            }


            public async Task<IDataResult<IEnumerable<SinavDto>>> Handle(GetSinavDtoQuery request, CancellationToken cancellationToken)
            {
                var result = _sinavRepository.GetSinavDto();
                return new SuccessDataResult<IEnumerable<SinavDto>>(result);
            }
        }
    }
}

