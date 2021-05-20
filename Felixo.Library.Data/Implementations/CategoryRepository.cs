using Felixo.Library.Entities.Models;
using Felixo.Library.Entities.Models.Context;
using Felixo.Library.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace Felixo.Library.Data.Implementations
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly AppDataContext _context;

        public CategoryRepository(AppDataContext context) : base(context)
        {
            _context = context;
        }
    }
}
