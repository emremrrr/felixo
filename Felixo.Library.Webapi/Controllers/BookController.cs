using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Felixo.Library.Entities.Models;
using Felixo.Library.Data.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Felixo.Library.Entities.DTO;
using Felixo.Library.Service.Query;
using Felixo.Library.Logging.Aspects;

namespace Felixo.Library.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ExceptionAspect]
    public class BookController : ControllerBase
    {
        private readonly IUnit _unit;
        private readonly IMediator _mediator;

        public BookController(
            IUnit unit,
            IMediator mediator)
        {
            _unit = unit;
            _mediator = mediator;
        }
        [HttpGet]
        [Route("GetBooks")]
        public async Task<IEnumerable<BookDTO>> GetBooks()=> await _mediator.Send(new GetBookQueryCommand());

        [HttpGet]
        [Route("GetBookDetailsById")]
        public async Task<Book> GetBookDetailsById(long Id) => await _unit.BookRepository.GetById(Id);
    }
}
