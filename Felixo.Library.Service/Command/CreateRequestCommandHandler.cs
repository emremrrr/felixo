using Felixo.Library.Entities.Models;
using Felixo.Library.Data.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Felixo.Library.Service.Command
{
    public class CreateRequestCommandHandler : IRequestHandler<CreateRequestCommand,Request>
    {
        private readonly IUnit _unit;

        public CreateRequestCommandHandler(IUnit unit)
        {
            _unit = unit;
        }

        public async Task<Request> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
        {
            return await _unit.RequestRepository.Add(request.Request);
        }
    }
}
