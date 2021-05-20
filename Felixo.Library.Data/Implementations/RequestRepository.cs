using Felixo.Library.Entities.Models;
using Felixo.Library.Entities.Models.Context;
using Felixo.Library.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Felixo.Library.Entities.DTO;

namespace Felixo.Library.Data.Implementations
{
    public class RequestRepository : Repository<Request>, IRequestRepository
    {
        private readonly AppDataContext _context;

        public RequestRepository(AppDataContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Request>> GetWithBooks()
        {
            var requests = await _context.Requests.Include(p => p.Book).Include(x => x.ApplicationUser).ToListAsync();
            return requests;

        }

    }
}
