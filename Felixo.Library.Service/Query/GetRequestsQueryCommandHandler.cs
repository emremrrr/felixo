using Felixo.Library.Entities.Models;
using Felixo.Library.Data.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Felixo.Library.Entities.DTO;
using System.Linq;

namespace Felixo.Library.Service.Query
{
    public class GetRequestsQueryCommandHandler : IRequestHandler<GetRequestsQueryCommand, IEnumerable<RequestDTO>>
    {
        private readonly IUnit _unit;
        private readonly IMapper _mapper;

        public GetRequestsQueryCommandHandler(IUnit unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RequestDTO>> Handle(GetRequestsQueryCommand request, CancellationToken cancellationToken)
        {
            var result = await _unit.RequestRepository.GetWithBooks();
            return _mapper.Map<IEnumerable<Request>, List<RequestDTO>>(result);
        }
    }
}
