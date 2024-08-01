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

namespace Business.Handlers.Dersliks.Queries
{
    public class GetDerslikDtoQuery:IRequest<IDataResult<IEnumerable<DerslikDto>>>
    {
        public class GetDerslikDtoQueryHandler : IRequestHandler<GetDerslikDtoQuery, IDataResult<IEnumerable<DerslikDto>>>
        {
            private readonly IDerslikRepository _derslikRepository;
            private readonly IMediator _mediator;

            public GetDerslikDtoQueryHandler(IDerslikRepository derslikRepository, IMediator mediator)
            {
                _derslikRepository = derslikRepository;
                _mediator = mediator;
            }

            public async Task<IDataResult<IEnumerable<DerslikDto>>> Handle(GetDerslikDtoQuery request, CancellationToken cancellationToken)
            {
                var result = _derslikRepository.GetDerslikDto();
                return new SuccessDataResult<IEnumerable<DerslikDto>>(result);
            }
        }
    }
}
