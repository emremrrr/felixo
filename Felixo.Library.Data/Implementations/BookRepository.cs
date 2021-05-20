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

namespace Felixo.Library.Data.Implementations
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private readonly AppDataContext _context;

        public BookRepository(AppDataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetBooksWithCategory()
        {
            var books =await _context.Books.Include(p => p.Category).ToListAsync();
            return books;
        }
    }
}
