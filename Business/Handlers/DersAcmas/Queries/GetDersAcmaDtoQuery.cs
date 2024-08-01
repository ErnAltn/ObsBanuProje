using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Handlers.DersAcmas.Queries
{
    public class GetDersAcmaDtoQuery:IRequest<IDataResult<IEnumerable<DersAcmaDto>>>
    {
        public class GetDersAcmaDtoQueryHandler : IRequestHandler<GetDersAcmaDtoQuery, IDataResult<IEnumerable<DersAcmaDto>>>
        {
            private readonly IDersAcmaRepository _dersAcmaRepository;
            private readonly IMediator _mediator;

            public GetDersAcmaDtoQueryHandler(IDersAcmaRepository dersAcmaRepository, IMediator mediator)
            {
                _dersAcmaRepository = dersAcmaRepository;
                _mediator = mediator;
            }

            public async Task<IDataResult<IEnumerable<DersAcmaDto>>> Handle(GetDersAcmaDtoQuery request, CancellationToken cancellationToken)
            {
                var result = _dersAcmaRepository.GetDersAcmaDtoAsync();
                return new SuccessDataResult<IEnumerable<DersAcmaDto>>(result,"DersAcmaDto getirildi");
            }
        }
    }
}
