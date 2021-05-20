using System;
using System.Collections.Generic;
using System.Text;
using Felixo.Library.Entities.Models;
using MediatR;

namespace Felixo.Library.Service.Command
{
    public class CreateRequestCommand:IRequest<Request>
    {
        public Request Request { get; set; }
    }
}
