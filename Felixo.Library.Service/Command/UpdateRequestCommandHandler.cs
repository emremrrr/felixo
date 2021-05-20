using Felixo.Library.Data.UnitOfWork;
using Felixo.Library.Entities.Models;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Felixo.Library.Service.Command
{
    public class UpdateRequestCommandHandler<T> : IRequestHandler<UpdateRequestCommand, Request> where T : Hub
    {
        private readonly IUnit _unit;
        private readonly IHubContext<T> _hubContext;

        public UpdateRequestCommandHandler(IUnit unit, IHubContext<T> hubContext)
        {
            _unit = unit;
            _hubContext = hubContext;
        }
        public async Task<Request> Handle(UpdateRequestCommand request, CancellationToken cancellationToken)
        {
            await _unit.RequestRepository.Update(request.Request, request.Request.Id);
            await _hubContext.Clients.Client(request.Request.ClientId).SendAsync("requestdata", request.Request);

            return request.Request;

        }
    }
}
