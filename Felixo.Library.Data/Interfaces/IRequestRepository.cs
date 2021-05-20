using Felixo.Library.Entities.DTO;
using Felixo.Library.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Felixo.Library.Data.Interfaces
{
    public interface IRequestRepository : IRepository<Request>
    {
         Task<IEnumerable<Request>> GetWithBooks();

    }
}
