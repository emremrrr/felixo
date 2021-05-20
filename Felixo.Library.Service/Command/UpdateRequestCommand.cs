using Felixo.Library.Entities.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Felixo.Library.Service.Command
{
    public class UpdateRequestCommand : IRequest<Request>
    {
        public UpdateRequestCommand()
        {
        }
        public Request? Request { get; set; }
    }
}
