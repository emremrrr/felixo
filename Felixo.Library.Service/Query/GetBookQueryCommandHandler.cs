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

namespace Felixo.Library.Service.Query
{
    public class GetBookQueryCommandHandler : IRequestHandler<GetBookQueryCommand, IEnumerable<BookDTO>>
    {
        private readonly IUnit _unit;
        private readonly IMapper _mapper;

        public GetBookQueryCommandHandler(IUnit unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<IEnumerable<BookDTO>> Handle(GetBookQueryCommand request, CancellationToken cancellationToken)
        {
            var result = await _unit.BookRepository.GetBooksWithCategory();
            return _mapper.Map<IEnumerable<Book>, List<BookDTO>>(result);
        }
    }
}
