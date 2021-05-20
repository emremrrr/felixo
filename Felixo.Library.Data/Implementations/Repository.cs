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
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDataContext _context;
        private readonly IMapper _mapper;

        public Repository(AppDataContext context)
        {
            _context = context;
        }

        public async Task<T> Add(T item)
        {
            await _context.Set<T>().AddAsync(item);
            return item;
        }

        public async Task Delete(long id)
        {
            var item = await _context.Set<T>().FindAsync(id);
            _context.Set<T>().Remove(item);
        }

        public async Task<IEnumerable<T>> Get() => await _context.Set<T>().ToListAsync();

        public async Task<T> GetById(long id) => await _context.Set<T>().FindAsync(id);

        public async Task<T> Update(T item, object Id)
        {
            T exist = await _context.Set<T>().FindAsync(Id);
            if (exist != null)
                _context.Entry(exist).CurrentValues.SetValues(item);
            return item;
            //_context.Set<T>().Update(item);
        }
    }
}
