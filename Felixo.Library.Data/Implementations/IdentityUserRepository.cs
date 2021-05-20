using Felixo.Library.Entities.Models;
using Felixo.Library.Entities.Models.Context;
using Felixo.Library.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;

namespace Felixo.Library.Data.Implementations
{
    public class IdentityUserRepository : Repository<ApplicationUser>, IidentityUser
    {
        private readonly AppDataContext _context;

        public IdentityUserRepository(AppDataContext context) : base(context)
        {
            _context = context;
        }

        public User GetUserWithRole(string email)
        {
            var user = _context.Users.Where(p => p.Email == email).Include(p=>p.UserRoles).ThenInclude(p=>p.Role).FirstOrDefault();
            //var UserRoles = _context.Roles;

            return new User()
            {
                IdentityUser=user,
                Roles = user.UserRoles
            };
        }
    }
}
