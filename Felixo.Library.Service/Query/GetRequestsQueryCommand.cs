using Felixo.Library.Entities.DTO;
using Felixo.Library.Entities.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Felixo.Library.Service.Query
{
    public class GetRequestsQueryCommand:IRequest<IEnumerable<RequestDTO>>
    {
    }
}
