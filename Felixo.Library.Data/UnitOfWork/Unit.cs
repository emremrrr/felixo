using Felixo.Library.Entities.Models.Context;
using Felixo.Library.Data.Implementations;
using Felixo.Library.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Felixo.Library.Data.UnitOfWork
{
    public class Unit : IUnit
    {
        private readonly AppDataContext _context;

        public Unit()
        {
            _context = new AppDataContext();
            //var x=_context.Roles.Find("");
            BookRepository = new BookRepository(_context);
            RequestRepository = new RequestRepository(_context);
            CategoryRepository = new CategoryRepository(_context);
            IdentityUserLoginRepository = new IdentityUserLoginRepository(_context);
            IdentityUserRepository = new IdentityUserRepository(_context);
            IdentityUserRoleRepository = new IdentityUserRoleRepository(_context);
        }

        public IBookRepository BookRepository { get; private set; }

        public IRequestRepository RequestRepository { get; private set; }

        public ICategoryRepository CategoryRepository { get; private set; }

        public IidentityUserLogin IdentityUserLoginRepository { get; private set; }

        public IidentityUser IdentityUserRepository { get; private set; }
        public IidentityUserRole IdentityUserRoleRepository { get; private set; }

        public IidentityRole IdentityRoleRepository { get; private set; }

        public async Task<int> Commit()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
